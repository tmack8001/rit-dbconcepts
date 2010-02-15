
using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace rit_dbconcepts
{


	public class View : System.Windows.Forms.Form
	{

		// Main Page
		private Label windowHeader;
		
		private TextBox movieTitle;
		private TextBox genre;
		private TextBox publisher;
		private TextBox castCrewMember;
		private ListBox results;
		
		private Button submit;
		
		// CusInfo Page
		private Label customerHeader;
		
		private TextBox customer;
		private TextBox address;
		
		private ListBox custResults;
		
		private Button custSubmit;
		
		// Tabbed Layout
		private TabControl tabs;
			
		public View ()
		{
			this.tabs = new TabControl ();
			
			// Create main page objects
			this.windowHeader = new Label ();
			this.movieTitle = new TextBox ();
			this.castCrewMember = new TextBox ();
			this.genre = new TextBox ();
			this.publisher = new TextBox ();
			this.results = new ListBox ();
			this.submit = new Button ();
			
			// Create CusInfo page objects
			this.customerHeader = new Label ();
			this.customer = new TextBox ();
			this.address = new TextBox ();
			this.custResults = new ListBox ();
			this.custSubmit = new Button ();
			
			// Set details
			this.SuspendLayout ();
			
			this.windowHeader.Location = new Point (325, 10);
			this.windowHeader.Name = "label1";
			this.windowHeader.Size = new Size (300, 20);
			this.windowHeader.Text = "Movie Database";
			
			// Movie Title Search
			this.movieTitle.Location = new Point (45, 50);
			this.movieTitle.Size = new Size (640, 32);
			this.movieTitle.Name = "MovieTitle";
			this.movieTitle.Text = "Enter a movie title";
			this.movieTitle.TabIndex = 1;
			
			// Cast/Crew Search
			this.castCrewMember.Location = new Point (45, 100);
			this.castCrewMember.Size = new Size (640, 32);
			this.castCrewMember.Name = "CastCrew";
			this.castCrewMember.Text = "Enter a cast or crew member";
			this.castCrewMember.TabIndex = 2;
			
			// Genre
			this.genre.Location = new Point (45, 150);
			this.genre.Size = new Size (640, 32);
			this.genre.Name = "Genre";
			this.genre.Text = "Enter a genre";
			this.genre.TabIndex = 3;
			
			// publisher
			this.publisher.Location = new Point (45, 200);
			this.publisher.Size = new Size (640, 32);
			this.publisher.Name = "Publisher";
			this.publisher.Text = "Enter a publisher";
			this.publisher.TabIndex = 4;
			
			
			//results ListBox
			this.results.Location = new Point (40, 250);
			this.results.Name = "Results";
			this.results.Size = new Size (700, 300);
			
			// Submit
			this.submit.Location = new Point (350, 550);
			this.submit.Name = "submitButton";
			this.TabIndex = 5;
			this.submit.Text = "Submit";
			this.submit.Click += delegate(object sender, EventArgs e) {
				Application.Exit ();
			};
			
			
			// ***** Customer Lookup init *****/
			this.customerHeader.Location = new Point (325, 10);
			this.customerHeader.Name = "CustomerHeader";
			this.customerHeader.Size = new Size (300, 20);
			this.customerHeader.Text = "Customer Lookup";
			
			this.customer.Location = new Point (45, 50);
			this.customer.Name = "CustomerField";
			this.customer.Size = new Size (640, 32);
			this.customer.Text = "Enter customer name";
			this.customer.TabIndex = 1;
			
			this.address.Location = new Point (45, 100);
			this.address.Name = "Address Field";
			this.address.Size = new Size (640, 32);
			this.address.TabIndex = 2;
			this.address.Text = "Enter an address to search";
			
			this.custResults.Location = new Point (40, 150);
			this.custResults.Name = "Results";
			this.custResults.Size = new Size (700, 300);
			
			// CustSubmit
			this.custSubmit.Location = new Point (350, 550);
			this.custSubmit.Name = "CustSubmitButton";
			this.custSubmit.TabIndex = 3;
			this.custSubmit.Text = "Lookup";
			this.custSubmit.Click += delegate(object sender, EventArgs e) { 
				Application.Exit (); 
			};
			
			// Set windows prefs and add components
			this.ClientSize = new Size (800, 600);
			TabPage Lookup = new TabPage ("Movie Lookup");
			
			Lookup.Controls.Add (this.windowHeader);
			Lookup.Controls.Add (this.movieTitle);
			Lookup.Controls.Add (this.castCrewMember);
			Lookup.Controls.Add (this.genre);
			Lookup.Controls.Add (this.publisher);
			Lookup.Controls.Add (this.submit);
			Lookup.Controls.Add (this.results);
			
			TabPage CusInfo = new TabPage ("Customer Lookup");
			CusInfo.Controls.Add (this.customerHeader);
			CusInfo.Controls.Add (this.address);
			CusInfo.Controls.Add (this.customer);
			CusInfo.Controls.Add (this.custResults);
			CusInfo.Controls.Add (this.custSubmit);
				
			tabs.TabPages.Add (Lookup);
			tabs.TabPages.Add (CusInfo);
			tabs.Size = new Size (800, 600);
			
			this.Controls.Add (this.tabs);
		}
		
	}
}
