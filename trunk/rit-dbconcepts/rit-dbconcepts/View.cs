
using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace rit_dbconcepts
{


    public class View : System.Windows.Forms.Form
    {

        private ArrayList cBoxList = new ArrayList();
		private Form info;
		private TabPage movieLookup;
		private TabPage custLookup;
		
		
        // Main Page
        private Label windowHeader;

        private TextBox storeLoc;
        private TextBox movieTitle;
        private TextBox genre;
        private TextBox publisher;
        private TextBox castCrewMember;
        private ListBox results;

        private Button submit;
		private Button infoButton;
		private Button transaction;

        // CusInfo Page
        private Label customerHeader;
		private Label selectedMovie;

        private TextBox customer;
        private TextBox address;
		private TextBox cardNum;

        private ListBox custResults;

        private Button custSubmit;
		private Button custInfoButton;

        // Tabbed Layout
        private TabControl tabs;

        public View ()
        {
        	this.tabs = new TabControl ();

            // Create main page objects
        	movieLookup = initMovieLookup ();
        	
            // Create CusInfo page object
        	custLookup = initCustLookup ();
        	
            // Set windows prefs and add components
        	this.ClientSize = new Size (800, 600);
        	
            tabs.Controls.Add (movieLookup);
        	tabs.Controls.Add (custLookup);

            tabs.Size = new Size (800, 600);
			
            this.Controls.Add (this.tabs);
        }

        private TabPage initMovieLookup ()
        {
        	TabPage Lookup = new TabPage ("Movie Lookup");

            this.windowHeader = new Label ();
        	this.storeLoc = new TextBox ();
        	this.movieTitle = new TextBox ();
        	this.castCrewMember = new TextBox ();
        	this.genre = new TextBox ();
        	this.publisher = new TextBox ();
        	this.results = new ListBox ();
        	this.submit = new Button ();
        	this.infoButton = new Button ();
        	this.transaction = new Button ();

            this.windowHeader.Location = new Point (325, 10);
        	this.windowHeader.Name = "label1";
        	this.windowHeader.Size = new Size (300, 20);
        	this.windowHeader.Text = "Movie Database";

            // Movie Title Search
        	this.movieTitle.Location = new Point (30, 50);
        	this.movieTitle.Size = new Size (640, 32);
        	this.movieTitle.Name = "MovieTitle";
        	this.movieTitle.Text = "Enter a movie title";
        	this.movieTitle.TabIndex = 1;
        	
        	// Location
        	this.storeLoc.Location = new Point (30, 100);
        	this.storeLoc.Size = new Size (640, 32);
        	this.storeLoc.Name = "LocationField";
        	this.storeLoc.Text = "Enter a town/city";
        	this.storeLoc.TabIndex = 2;
        	
        	// Cast/Crew Search
        	this.castCrewMember.Location = new Point (30, 150);
        	this.castCrewMember.Size = new Size (640, 32);
        	this.castCrewMember.Name = "CastCrew";
        	this.castCrewMember.Text = "Enter a cast or crew member";
        	this.castCrewMember.TabIndex = 3;
        	
        	// Genre
        	this.genre.Location = new Point (30, 200);
        	this.genre.Size = new Size (640, 32);
        	this.genre.Name = "Genre";
        	this.genre.Text = "Enter a genre";
        	this.genre.TabIndex = 4;
        	
        	// publisher
        	this.publisher.Location = new Point (30, 250);
        	this.publisher.Size = new Size (640, 32);
        	this.publisher.Name = "Publisher";
        	this.publisher.Text = "Enter a publisher";
        	this.publisher.TabIndex = 5;
        	
        	// CheckBoxes
        	int xloc = 680;
        	int yloc = 70;
        	int i = 0;
        	while (i < 8) {
        		CheckBox box = new CheckBox ();
        		box.Location = new Point (xloc, yloc);
        		box.Size = new Size (45, 32);
        		if (i < 4) {
        			box.Text = "AND";
        		} else {
        			box.Text = "OR";
        		}
        		this.cBoxList.Add (box);
        		
        		if (i == 3) {
        			xloc += 50;
        			yloc = 70;
        		} else {
        			yloc += 50;
        		}
        		i++;
        	}
        	
        	//results ListBox
        	this.results.Location = new Point (40, 300);
        	this.results.Name = "Results";
        	this.results.Size = new Size (700, 250);
        	this.results.SelectedValueChanged += delegate {
        		if (results.Items.Count > 0) {
        			this.infoButton.Enabled = true;
        			this.transaction.Enabled = true;
        		} else {
        			this.infoButton.Enabled = false;
					this.transaction.Enabled = false;
        		}
        	};
        	
        	// Submit
        	this.submit.Location = new Point (250, 550);
        	this.submit.Name = "submitButton";
        	this.submit.TabIndex = 5;
        	this.submit.Text = "Submit";
        	this.submit.Click += delegate(object sender, EventArgs e) { 
                Application.Exit (); 
            };
			
			// infoButton
			this.infoButton.Location = new Point (320, 550);
			this.infoButton.Name = "InfoButton";
			this.infoButton.TabIndex = 5;
			this.infoButton.Text = "Info";
			this.infoButton.Enabled = false;
			this.infoButton.Click += delegate(object sender, EventArgs e) { 
				foreach( String cur in results.Items ){
					infoPanel ("Movie", cur);
				}
			};
			
			// transaction
			this.transaction.Location = new Point (390, 550);
			this.transaction.Name = "InfoButton";
			this.transaction.TabIndex = 5;
			this.transaction.Text = "Transaction";
			this.transaction.Enabled = false;
			this.transaction.Click += delegate(object sender, EventArgs e) {
				this.selectedMovie.Text = (String)this.results.SelectedItem;
				tabs.SelectedTab = this.custLookup;
			};

            Lookup.Controls.Add (this.windowHeader);
            Lookup.Controls.Add (this.movieTitle);
            Lookup.Controls.Add (this.storeLoc);
            Lookup.Controls.Add (this.castCrewMember);
            Lookup.Controls.Add (this.genre);
            Lookup.Controls.Add (this.publisher);
            foreach (CheckBox b in this.cBoxList) {
                Lookup.Controls.Add (b);
            }
            Lookup.Controls.Add (this.results);
            Lookup.Controls.Add (this.submit);
			Lookup.Controls.Add( this.infoButton);
			Lookup.Controls.Add( this.transaction );
			
			// TODO remove
			results.Items.Add( "Matrix");

            return Lookup;
        }

        private TabPage initCustLookup ()
        {
        	TabPage CusInfo = new TabPage ("Customer Lookup");
        	this.customerHeader = new Label ();
        	this.selectedMovie = new Label ();
        	this.customer = new TextBox ();
        	this.address = new TextBox ();
        	this.cardNum = new TextBox ();
        	this.custResults = new ListBox ();
        	this.custSubmit = new Button ();
        	this.custInfoButton = new Button ();

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

			this.cardNum.Location = new Point (45, 150);
        	this.cardNum.Name = "CardNum";
        	this.cardNum.Size = new Size (640, 32);
        	this.cardNum.TabIndex = 3;
        	this.cardNum.Text = "Enter the customers card number";
    
            this.custResults.Location = new Point (40, 200);
        	this.custResults.Name = "Results";
        	this.custResults.Size = new Size (700, 300);
			this.custResults.SelectedValueChanged += delegate {
				if (results.Items.Count > 0) {
					this.custInfoButton.Enabled = true;
				} else {
					this.custInfoButton.Enabled = false;
				}
			};

            // CustSubmit
        	this.custSubmit.Location = new Point (300, 550);
        	this.custSubmit.Name = "CustSubmitButton";
        	this.custSubmit.TabIndex = 3;
        	this.custSubmit.Text = "Lookup";
        	this.custSubmit.Click += delegate(object sender, EventArgs e) {
                Application.Exit (); 
            };
			
			// CustSubmit
        	this.custInfoButton.Location = new Point (375, 550);
        	this.custInfoButton.Name = "CustInfoButton";
        	this.custInfoButton.TabIndex = 4;
        	this.custInfoButton.Text = "Info";
			this.custInfoButton.Enabled = false;
        	this.custInfoButton.Click += delegate(object sender, EventArgs e) {
                foreach( String sel in custResults.Items){
					infoPanel("Customer", sel); 
				}
            };
        	
        	CusInfo.Controls.Add (this.customerHeader);
        	CusInfo.Controls.Add (this.address);
        	CusInfo.Controls.Add (this.customer);
        	CusInfo.Controls.Add (this.cardNum);
        	CusInfo.Controls.Add (this.custResults);
        	CusInfo.Controls.Add (this.custSubmit);
			CusInfo.Controls.Add (this.custInfoButton);
			
			// TODO remove
			custResults.Items.Add( "Smith, Bill");

            return CusInfo;
        }

        public void infoPanel (String title, String selection)
        {

            this.info = new Form ();
        	this.info.ClientSize = new Size (500, 350);
        	
			Label infoPanelHeader = new Label ();
        	infoPanelHeader.Text = title + " Info";
        	infoPanelHeader.Location = new Point (200, 20);
        	this.info.Controls.Add (infoPanelHeader);
   
			Label L1 = new Label ();
        	L1.Location = new Point (30, 50);
        	L1.Size = new Size (60, 32);
        	TextBox T1 = new TextBox ();
        	T1.Size = new Size (335, 32);
        	T1.Location = new Point (90, 50);

            Label L2 = new Label ();
        	L2.Location = new Point (30, 100);
        	L2.Size = new Size (60, 32);
        	TextBox T2 = new TextBox ();
        	T2.Size = new Size (335, 32);
        	T2.Location = new Point (90, 100);

            Label L3 = new Label ();
        	L3.Location = new Point (30, 150);
        	L3.Size = new Size (60, 32);
        	TextBox T3 = new TextBox ();
        	T3.Size = new Size (335, 32);
        	T3.Location = new Point (90, 150);

            Label L4 = new Label ();
        	L4.Location = new Point (30, 200);
        	L4.Size = new Size (60, 32);
        	TextBox T4 = new TextBox ();
        	T4.Size = new Size (335, 32);
        	T4.Location = new Point (90, 200);

			Label L5 = new Label ();
        	L5.Location = new Point (30, 250);
        	L5.Size = new Size (60, 32);
        	TextBox T5 = new TextBox ();
        	T5.Size = new Size (335, 32);
        	T5.Location = new Point (90, 250);
   
			
			if (title.ToLower ().Equals ("movie")) 
			{
        		L1.Text = "Title";
        		info.Controls.Add (L1);
        		T1.Text = selection;
        		info.Controls.Add (T1);
    
				L2.Text = "StoreId";
        		info.Controls.Add (L2);
        		info.Controls.Add (T2);
   
				L3.Text = "Release Date";
        		info.Controls.Add (L3);
        		info.Controls.Add (T3);
    
				L4.Text = "Genre";
        		info.Controls.Add (L4);
        		info.Controls.Add (T4);
				
				L5.Text = "Cast/Crew";
				info.Controls.Add (L5);
				info.Controls.Add (T5);
			} else if( title.ToLower().Equals( "customer" )){
				
				L1.Text = "Name (Last/First)";
				info.Controls.Add (L1);
				info.Controls.Add (T1);
				
				L2.Text = "Address";
				info.Controls.Add (L2);
				info.Controls.Add (T2);
				
				L3.Text = "Card Number";
				info.Controls.Add (L3);
				info.Controls.Add (T3);
				
			}
			
			Button addItem = new Button();
			addItem.Location = new Point( 200, 300);
			addItem.Text = "Add";			
			addItem.Click +=  delegate(object sender, EventArgs e) {
				this.info.Dispose();
			};
			this.info.Controls.Add( addItem );
						
			this.info.Visible = true;
		}
    }

}
