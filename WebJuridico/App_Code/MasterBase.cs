using DACJuridico;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MasterBase
/// </summary>
public class MasterBase : System.Web.UI.MasterPage
{


    #region Campos

    private static DataAcces dataAcces;

    #endregion


	public MasterBase()
        :base()
	{
		//
		// TODO: Add constructor logic here
		//
	}



    #region Propiedades

    protected static DataAcces DataAcces
    {
        get
        {
            if (dataAcces == null)
            {
                dataAcces = new DataAcces();
            }

            return dataAcces;
        }
    }

    #endregion
}