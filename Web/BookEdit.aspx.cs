using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Inflectra.SampleApplications.LibraryInformationSystem.Web.DataObjects;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web
{
    public partial class BookEdit : PageBase
    {
        protected Book book = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //See if we have an existing book to get
            GetBook();

            //Register event handlers
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.btnSubmit.Click += new EventHandler(btnSubmit_Click);

            if (!IsPostBack)
            {
                //Load and bind the dropdown lists
                this.ddlAuthor.DataSource = Authors;
                this.ddlAuthor.DataBind();
                this.ddlGenre.DataSource = Genres;
                this.ddlGenre.DataBind();

                //Display the appropriate insert/update content
                if (book == null)
                {
                    this.plcEditBook.Visible = false;
                    this.plcNewBook.Visible = true;
                    this.btnSubmit.Text = "Insert";
                }
                else
                {
                    this.plcEditBook.Visible = true;
                    this.plcNewBook.Visible = false;
                    this.btnSubmit.Text = "Update";

                    //Populate the form with existing data
                    this.txtName.Text = this.book.Name;
                    this.ddlAuthor.SelectedValue = this.book.AuthorId.ToString();
                    this.ddlGenre.SelectedValue = this.book.GenreId.ToString();
                }
            }
        }

        /// <summary>
        /// Tries to load the book if provided
        /// </summary>
        protected void GetBook()
        {
            //See if we are passed an book id
            if (!String.IsNullOrWhiteSpace(Request.QueryString["id"]))
            {
                int intValue;
                if (Int32.TryParse(Request.QueryString["id"], out intValue))
                {
                    int bookId = intValue;

                    //Try and get this book from session
                    this.book = Books.FirstOrDefault(b => b.Id == bookId);
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

            //Get the book if provided
            GetBook();

            //Collect the data from the form and either insert or update
            if (book == null)
            {
                //Need to find the book with the maximum ID currently and increment
                int maxIdBook = Books.OrderByDescending(b => b.Id).First().Id + 1;

                Book newBook = new Book();
                newBook.Id = maxIdBook;
                newBook.Name = txtName.Text.Trim();
                newBook.AuthorId = Int32.Parse(ddlAuthor.SelectedValue);
                newBook.GenreId = Int32.Parse(ddlGenre.SelectedValue);
                newBook.DateAdded = DateTime.UtcNow;
                Books.Add(newBook);
            }
            else
            {
                book.Name = txtName.Text.Trim();
                book.AuthorId = Int32.Parse(ddlAuthor.SelectedValue);
                book.GenreId = Int32.Parse(ddlGenre.SelectedValue);
            }

            //Return to list
            Response.Redirect("Books.aspx");
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            //Return to list
            Response.Redirect("Books.aspx");
        }
    }
}