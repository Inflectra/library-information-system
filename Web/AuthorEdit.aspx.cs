using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Inflectra.SampleApplications.LibraryInformationSystem.Web.DataObjects;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web
{
    public partial class AuthorEdit : PageBase
    {
        protected Author author = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //See if we have an existing author to get
            GetAuthor();

            //Register event handlers
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.btnSubmit.Click += new EventHandler(btnSubmit_Click);

            if (!IsPostBack)
            {
                //Display the appropriate insert/update content
                if (author == null)
                {
                    this.plcEditAuthor.Visible = false;
                    this.plcNewAuthor.Visible = true;
                    this.btnSubmit.Text = "Insert";
                }
                else
                {
                    this.plcEditAuthor.Visible = true;
                    this.plcNewAuthor.Visible = false;
                    this.btnSubmit.Text = "Update";

                    //Populate the form with existing data
                    this.txtName.Text = this.author.Name;
                    this.txtAge.Text = this.author.Age.ToString();
                }
            }
        }

        /// <summary>
        /// Tries to load the author if provided
        /// </summary>
        protected void GetAuthor()
        {
            //See if we are passed an author id
            if (!String.IsNullOrWhiteSpace(Request.QueryString["id"]))
            {
                int intValue;
                if (Int32.TryParse(Request.QueryString["id"], out intValue))
                {
                    int authorId = intValue;

                    //Try and get this author from session
                    this.author = Authors.FirstOrDefault(a => a.Id == authorId);
                }
            }
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            //Make sure is valid
            if (!IsValid)
            {
                return;
            }

            //Get the author if provided
            GetAuthor();

            //Collect the data from the form and either insert or update
            if (author == null)
            {
                //Need to find the author with the maximum ID currently and increment
                int maxIdAuthor = Authors.OrderByDescending(a => a.Id).First().Id + 1;

                Author newAuthor = new Author();
                newAuthor.Id = maxIdAuthor;
                newAuthor.Name = txtName.Text.Trim();
                newAuthor.Age = Int32.Parse(txtAge.Text.Trim());
                Authors.Add(newAuthor);
            }
            else
            {
                author.Name = txtName.Text.Trim();
                author.Age = Int32.Parse(txtAge.Text.Trim());
            }

            //Return to list
            Response.Redirect("Authors.aspx");
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            //Return to list
            Response.Redirect("Authors.aspx");
        }
    }
}