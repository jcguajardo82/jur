using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System.Xml.Linq;
using DocumentFormat.OpenXml;
using Entidades;

namespace DACJuridico
{
    public class OfficeDataAcces
    {

        #region Campos

        #endregion

        #region Constructor

        public OfficeDataAcces()
        {
        }

        #endregion

        #region Propiedades

        #endregion

        #region Metodos

        public byte[] CheckFileIntegrity(byte[] archivo, ref Exception ex)
        {
            Body body;

            //MemoryStream ms;
            //ms = new MemoryStream(archivo);

            MemoryStream ms = new MemoryStream();
            ms.Write(archivo, 0, (int)archivo.Length);

            try
            {
                using (WordprocessingDocument ws = WordprocessingDocument.Open(ms, true))
                {
                    body = ws.MainDocumentPart.Document.Body;

                    foreach (OpenXmlElement xelem in body.ChildElements)
                    {
                        //GetRecurrentChild(xelem, etiquetas);
                    }

                    ws.Close();

                    return ms.ToArray();
                }
            }
            catch (Exception exc)
            {
                ex = exc;
                return null;
            }
            finally
            {
                ms.Flush();
                ms.Close();
                ms.Dispose();
            }
        }

        public byte[] ReplaceOpenFormat(List<SolicitudEtiqueta> etiquetas, PlantillaArchivo archivo, ref Exception ex)
        {
            Body body;

            //MemoryStream ms;
            //ms = new MemoryStream(archivo.Archivo);

            MemoryStream ms = new MemoryStream();
            ms.Write(archivo.Archivo, 0, (int)archivo.Archivo.Length);

            try
            {
                using (WordprocessingDocument ws = WordprocessingDocument.Open(ms, true))
                {
                    body = ws.MainDocumentPart.Document.Body;

                    foreach (OpenXmlElement xelem in body.ChildElements)
                    {
                        GetRecurrentChild(xelem, etiquetas);
                    }

                    ws.Close();

                    return ms.ToArray();
                }
            }
            catch (Exception exc)
            {
                ex = exc;
                return null;
            }
            finally
            {
                ms.Flush();
                ms.Close();
                ms.Dispose();
            }
        }

        private void GetRecurrentChild(OpenXmlElement el, List<SolicitudEtiqueta> etiquetas)
        {
            List<SolicitudEtiqueta> aux;
            string nuevoTexto;

            if (el.HasChildren)
            {
                foreach (OpenXmlElement e in el.ChildElements)
                {
                    GetRecurrentChild(e, etiquetas);
                }
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(el.InnerXml))
                    {
                        if (el.GetType().Name.ToString().EndsWith("Text"))
                        {
                            aux = etiquetas.Where(x => el.InnerXml.Contains(x.Etiqueta)).ToList();
                            nuevoTexto = string.Empty;

                            if (aux.Count > 0)
                            {
                                aux.ForEach(x =>
                                    {
                                        if (string.IsNullOrEmpty(nuevoTexto))
                                        {
                                            nuevoTexto = el.InnerXml.Replace(x.Etiqueta, x.Valor);
                                        }
                                        else
                                        {
                                            nuevoTexto = nuevoTexto.Replace(x.Etiqueta, x.Valor);
                                        }
                                    });

                                el.Parent.AppendChild(new Text(nuevoTexto));
                                el.Parent.RemoveChild(el);
                            }

                            return;
                        }
                        else
                        {
                            el.GetType().Name.ToString();
                        }
                    }
                }
                catch
                {

                }
            }
        }

        #endregion

    }
}
