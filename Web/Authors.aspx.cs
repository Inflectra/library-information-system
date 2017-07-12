using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web
{
    public partial class Authors : PageBase
    {
        /// <summary>
        /// Display the list of authors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.grdAuthors.DataSource = Authors;
                this.grdAuthors.DataBind();
            }
        }
    }
}