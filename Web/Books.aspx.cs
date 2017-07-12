using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web
{
    public partial class Books : PageBase
    {
        /// <summary>
        /// Display the list of books
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Only display the create books link for the librarian user
            if (User.Identity.IsAuthenticated && User.Identity.Name.ToLowerInvariant() == "librarian")
            {
                this.plcCreateBook.Visible = true;
                this.grdBooks.Columns[4].Visible = true;
            }
            else
            {
                this.plcCreateBook.Visible = false;
                this.grdBooks.Columns[4].Visible = false;
            }

            if (!IsPostBack)
            {
                this.grdBooks.DataSource = Books;
                this.grdBooks.DataBind();
            }
        }
    }
}