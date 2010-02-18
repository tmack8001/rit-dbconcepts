
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

using rit_dbconcepts.Types;

namespace rit_dbconcepts
{


    public class View : System.Windows.Forms.Form
    {

		private Form info;
		private TabPage movieLookup;
		private TabPage custLookup;
        private TabPage empLookup;
		private DataAccessLayer DAL;
		
		
        // Main Page
        private Label windowHeader;

        private TextBox storeLoc;
        private TextBox movieTitle;
        private TextBox genre;
        private TextBox publisher;
        private ListBox results;

        private Button submit;
		private Button infoButton;
		private Button transaction;
		private Button addNewMovie;

        // CusInfo Page
        private Label customerHeader;
		private Label selectedMovie;

        private TextBox customer;
        private TextBox address;
		private TextBox cardNum;

        private ListBox custResults;

        private Button custSubmit;
		private Button custInfoButton;
		private Button custCheckIO;
		private Button addNewCust;

        //EmpInfo Page
        private Label employeeHeader;

        private TextBox employee;
        private TextBox position;

        private ListBox empResults;

        private Button empSubmit;
        private Button empInfoButton;
		
        // Tabbed Layout
        private TabControl tabs;
		

        public View ()
        {
        	
			this.tabs = new TabControl ();
        	this.Text = "MovieDatabase";
        	this.DAL = new DataAccessLayer ();
			
            // Create main page objects
        	movieLookup = initMovieLookup ();
        	
            // Create CusInfo page object
        	custLookup = initCustLookup ();

            // Create EmpInfo page object
            empLookup = initEmpLookup();
        	
            // Set windows prefs and add components
        	this.ClientSize = new Size (800, 600);
        	
            tabs.Controls.Add (movieLookup);
        	tabs.Controls.Add (custLookup);
            tabs.Controls.Add(empLookup);

            tabs.Size = new Size (800, 600);
			
            this.Controls.Add (this.tabs);
        }

        private TabPage initMovieLookup ()
        {
        	TabPage Lookup = new TabPage ("Movie Lookup");

            this.windowHeader = new Label ();
        	this.storeLoc = new TextBox ();
        	this.movieTitle = new TextBox ();
        	this.genre = new TextBox ();
        	this.publisher = new TextBox ();
        	this.results = new ListBox ();
        	this.submit = new Button ();
        	this.infoButton = new Button ();
        	this.transaction = new Button ();
        	this.addNewMovie = new Button ();

            this.windowHeader.Location = new Point (325, 10);
        	this.windowHeader.Name = "label1";
        	this.windowHeader.Size = new Size (300, 20);
        	this.windowHeader.Text = "Movie Database";

            // Movie Title Search
        	this.movieTitle.Location = new Point (30, 50);
        	this.movieTitle.Size = new Size (700, 32);
        	this.movieTitle.Name = "MovieTitle";
        	this.movieTitle.Text = "Enter a movie title";
        	this.movieTitle.TabIndex = 1;
        	
        	// Location
        	this.storeLoc.Location = new Point (30, 100);
        	this.storeLoc.Size = new Size (700, 32);
        	this.storeLoc.Name = "LocationField";
        	this.storeLoc.Text = "Enter a town/city";
        	this.storeLoc.TabIndex = 2;
        	
        	// Genre
        	this.genre.Location = new Point (30, 150);
        	this.genre.Size = new Size (700, 32);
        	this.genre.Name = "Genre";
        	this.genre.Text = "Enter a genre";
        	this.genre.TabIndex = 4;
        	
        	// publisher
        	this.publisher.Location = new Point (30, 200);
        	this.publisher.Size = new Size (700, 32);
        	this.publisher.Name = "Publisher";
        	this.publisher.Text = "Enter a publisher";
        	this.publisher.TabIndex = 5;
        	
        	//results ListBox
        	this.results.Location = new Point (30, 250);
        	this.results.Name = "Results";
        	this.results.Size = new Size (700, 300);
        	this.results.SelectedValueChanged += delegate {
        		if (results.Items.Count > 0) {
        			this.infoButton.Enabled = true;
        			this.transaction.Enabled = true;
        		} else {
        			this.infoButton.Enabled = false;
        			this.transaction.Enabled = false;
        		}
        	};
        	results.Items.Add ("Add a new movie");
        	
        	// Submit
        	this.submit.Location = new Point (250, 550);
        	this.submit.Name = "submitButton";
        	this.submit.TabIndex = 5;
        	this.submit.Text = "Submit";
        	this.submit.Click += delegate(object sender, EventArgs e) {
        		
        		this.results.Items.Clear ();
        		List<Movie> moviesInDb = new List<Movie> ();
        		
        		bool incTitle = !this.movieTitle.Text.Trim ().Equals ("Enter a movie title");
        		bool incStoreLoc = !this.storeLoc.Text.Trim ().Equals ("Enter a town/city");
        		bool incGenre = !this.genre.Text.Trim ().Equals ("Enter a genre");
        		bool incPublisher = !this.publisher.Text.Trim ().Equals ("Enter a publisher");
        		
        		if (this.movieTitle.Text.Trim ().Equals ("")) {
        			incTitle = false;
        		}
        		if (this.storeLoc.Text.Trim ().Equals ("")) {
        			incStoreLoc = false;
        		}
				if (this.genre.Text.Trim ().Equals ("")) {
        			incGenre = false;
        		}
        		if (this.publisher.Text.Trim ().Equals ("")) {
        			incPublisher = false;
				}
        				
        		if (incTitle && incStoreLoc) {
        			List<Store> store = DAL.getStoreByCity (this.storeLoc.Text.Trim ());
        			foreach (Store s in DAL.getStoreByCity (storeLoc.Text)) {
        				foreach (StockItem si in s.Inventory) {
        					if (si.Item.Movie.Title.Equals (this.movieTitle.Text.Trim ())) {
        						moviesInDb.Add (si.Item.Movie);
        					}
        				}
					}
				} else if (incTitle) {
        			
        			if (incStoreLoc) {
						List<Movie> found = new List<Movie> ();
        				List<Store> store = DAL.getStoreByCity (this.storeLoc.Text.Trim ());
        				Store s = null;
        				Movie m = null;
        				for (int j = store.Count - 1; j >= 0; j--) {
        					s = store[j];
        					for (int i = moviesInDb.Count - 1; i >= 0; i--) {
        						List<StockItem> invet = new List<StockItem> (s.Inventory);
        						StockItem st = null;
        						m = moviesInDb[i];
        						for (int k = invet.Count - 1; k >= 0; k--) {
        							st = invet[k];
        							if (st.Item.Movie.Title.Equals (m.Title)) {
        								found.Add (m);
        							}
        						}
        					}
        				}
        				moviesInDb = found;
        			} else { 
						moviesInDb = this.DAL.getMovieByTitle (this.movieTitle.Text.Trim ());
					}
         		} else if (incStoreLoc) {
        			
        			Store current = null;
        			if (this.DAL.getStoreByCity (this.storeLoc.Text.Trim ()).Count > 1) {
        				System.Console.Out.WriteLine ("Found multiple stores");
						Form picker = new Form ();
        				picker.Text = "Select a store";
        				picker.Size = new Size (400, 300);
        				
        				ListBox choices = new ListBox ();
        				choices.Location = new Point (5, 5);
        				choices.Size = new Size (400, 300);
        				foreach (Store s in this.DAL.getStoreByCity (this.storeLoc.Text.Trim ())) {
        					choices.Items.Add (s.Address);
        				}
        				
        				picker.Controls.Add (choices);
        				
        				Button accept = new Button ();
        				accept.Text = "Ok";
        				accept.Location = new Point (55, 45);
        				accept.Click += delegate {
        					int index = choices.SelectedIndex;
        					current = this.DAL.getStoreByCity (this.storeLoc.Text.Trim ())[index];
        					if (current != null) {
        						picker.Dispose ();
        					}
        				};
        				
						picker.Visible = true;
						
						
					} else if( this.DAL.getStoreByCity (this.storeLoc.Text.Trim ()).Count == 1){
						current = this.DAL.getStoreByCity (this.storeLoc.Text.Trim ())[ 0 ];
					} else {
						return;
					}
        			if (incTitle) {
						foreach (StockItem d in current.Inventory) {
							if (d.Item.Movie.Title.Equals (this.movieTitle.Text.Trim ())) {
								moviesInDb.Add( d.Item.Movie);
							}
						}
					} else {
						System.Console.Out.WriteLine ( current.Address.City + ": " + current.Inventory.Count );
						foreach( StockItem d in current.Inventory ){
							moviesInDb.Add( d.Item.Movie);
						}
					}
				} else if( incPublisher){
					
					if( incTitle ){
						List<Movie> match = new List<Movie>();
						foreach( Movie m in DAL.getMoviesByPublisher( this.publisher.Text.Trim() )){
							if( m.Title.Equals( this.movieTitle.Text.Trim())){ 
								match.Add( m );
							}
						}
						moviesInDb = match;
					} else {
						moviesInDb = DAL.getMoviesByPublisher( this.publisher.Text.Trim());
					}
					
				} else if( incGenre ){
					moviesInDb = DAL.getMoviesByGenre( this.genre.Text );
				} else
                {
                    moviesInDb = DAL.getMovies();
                }
				
				foreach( Movie m in moviesInDb){
					results.Items.Add(m);
				}
			
            };
			
			// infoButton
			this.infoButton.Location = new Point (325, 550);
			this.infoButton.Name = "InfoButton";
			this.infoButton.TabIndex = 5;
			this.infoButton.Text = "Info";
			this.infoButton.Enabled = false;
			this.infoButton.Click += delegate(object sender, EventArgs e) { 
				foreach( Movie cur in results.SelectedItems ){
					infoPanel ("Movie", cur);
				}
			};
			
			// transaction
			this.transaction.Location = new Point (400, 550);
			this.transaction.Name = "InfoButton";
			this.transaction.TabIndex = 5;
			this.transaction.Text = "Transaction";
			this.transaction.Enabled = false;
			this.transaction.Click += delegate(object sender, EventArgs e) {
				this.selectedMovie.Text = "Selected Movie: " + (String)this.results.SelectedItem;
				this.selectedMovie.Visible = true;
				tabs.SelectedTab = this.custLookup;
			};

			this.addNewMovie.Location = new Point (475, 550);
			this.addNewMovie.Name = "AddNewMovie";
			this.addNewMovie.Text = "Add New";
			this.addNewMovie.Click += delegate(object sender, EventArgs e) {
				infoPanel( "Movie", null);
			};
			
			
            Lookup.Controls.Add (this.windowHeader);
            Lookup.Controls.Add (this.movieTitle);
            Lookup.Controls.Add (this.storeLoc);
            Lookup.Controls.Add (this.genre);
            Lookup.Controls.Add (this.publisher);
            
            Lookup.Controls.Add (this.results);
            Lookup.Controls.Add (this.submit);
			Lookup.Controls.Add( this.infoButton);
			Lookup.Controls.Add( this.transaction );
			Lookup.Controls.Add( this.addNewMovie);

            return Lookup;
        }

        private TabPage initEmpLookup()
        {
            TabPage EmpInfo = new TabPage("Employee Lookup");
            this.employeeHeader = new Label();
            this.employee = new TextBox();
            this.position = new TextBox();
            this.empResults = new ListBox();
            this.empSubmit = new Button();
            this.empInfoButton = new Button();

            this.employeeHeader.Location = new Point(325, 10);
            this.employeeHeader.Name = "EmployeeHeader";
            this.employeeHeader.Size = new Size(300, 20);
            this.employeeHeader.Text = "Employee Lookup";

            this.employee.Location = new Point(30, 50);
            this.employee.Name = "EmployeeField";
            this.employee.Size = new Size(700, 32);
            this.employee.Text = "Enter employee name";
            this.employee.TabIndex = 1;

            this.position.Location = new Point(30, 100);
            this.position.Name = "PositionField";
            this.position.Size = new Size(700, 32);
            this.position.TabIndex = 2;
            this.position.Text = "Enter a position to search";

            this.empResults.Location = new Point(30, 200);
            this.empResults.Name = "Results";
            this.empResults.Size = new Size(700, 300);
            this.empResults.SelectedValueChanged += delegate
            {
                if (empResults.Items.Count > 0)
                {
                    this.empInfoButton.Enabled = true;
                }
                else
                {
                    this.empInfoButton.Enabled = false;
                }
            };
            empResults.Items.Add("Add a new customer");

            // CustSubmit
            this.empSubmit.Location = new Point(250, 550);
            this.empSubmit.Name = "EmpSubmitButton";
            this.empSubmit.TabIndex = 3;
            this.empSubmit.Text = "Lookup";

            this.empSubmit.Click += delegate(object sender, EventArgs e)
            {
                String[] names = { "", "" };
                if (this.employee.Text.Trim().IndexOf(" ") > 0)
                {
                    names = this.employee.Text.Trim().Split();
                }
                else
                {
                    names[1] = this.employee.Text.Trim();
                }

                this.empResults.Items.Clear();
                foreach (Employee emp in DAL.getEmployeesByFullName(names[0], names[1]))
                {
                    this.empResults.Items.Add(emp);
                }

            };

            // CustInfoButton
            this.empInfoButton.Location = new Point(325, 550);
            this.empInfoButton.Name = "EmpInfoButton";
            this.empInfoButton.TabIndex = 4;
            this.empInfoButton.Text = "Info";
            this.empInfoButton.Enabled = false;
            this.empInfoButton.Click += delegate(object sender, EventArgs e)
            {
                foreach (Employee sel in this.empResults.SelectedItems)
                {
                    infoPanel("Employee", sel);
                }
            };

            EmpInfo.Controls.Add(this.employeeHeader);
            EmpInfo.Controls.Add(this.employee);
            EmpInfo.Controls.Add(this.position);
            EmpInfo.Controls.Add(this.empResults);
            EmpInfo.Controls.Add(this.empSubmit);
            EmpInfo.Controls.Add(this.empInfoButton);

            return EmpInfo;
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
        	this.custCheckIO = new Button ();
        	this.addNewCust = new Button ();

            this.customerHeader.Location = new Point (325, 10);
        	this.customerHeader.Name = "CustomerHeader";
        	this.customerHeader.Size = new Size (300, 20);
        	this.customerHeader.Text = "Customer Lookup";
   
			this.selectedMovie.Location = new Point (20, 10);
        	this.selectedMovie.Name = "SelectedMovieLabel";
        	this.selectedMovie.Size = new Size (200, 30);
        	this.selectedMovie.Visible = false;

            this.customer.Location = new Point (30, 50);
        	this.customer.Name = "CustomerField";
        	this.customer.Size = new Size (700, 32);
        	this.customer.Text = "Enter customer name";
        	this.customer.TabIndex = 1;

            this.address.Location = new Point (30, 100);
        	this.address.Name = "Address Field";
        	this.address.Size = new Size (700, 32);
        	this.address.TabIndex = 2;
        	this.address.Text = "Enter an address to search";

			this.cardNum.Location = new Point (30, 150);
        	this.cardNum.Name = "CardNum";
        	this.cardNum.Size = new Size (700, 32);
        	this.cardNum.TabIndex = 3;
        	this.cardNum.Text = "Enter the customers card number";
    
            this.custResults.Location = new Point (30, 200);
        	this.custResults.Name = "Results";
        	this.custResults.Size = new Size (700, 300);
        	this.custResults.SelectedValueChanged += delegate {
        		if (custResults.Items.Count > 0) {
        			this.custInfoButton.Enabled = true;
        			this.custCheckIO.Enabled = true && selectedMovie.Visible;
        		} else {
        			this.custInfoButton.Enabled = false;
        			this.custCheckIO.Enabled = false;
        		}
        	};
        	custResults.Items.Add ("Add a new customer");

            // CustSubmit
        	this.custSubmit.Location = new Point (250, 550);
        	this.custSubmit.Name = "CustSubmitButton";
        	this.custSubmit.TabIndex = 3;
        	this.custSubmit.Text = "Lookup";
        	
        	this.custSubmit.Click += delegate(object sender, EventArgs e) {
                String[] names = {"", ""};
                if( customer.Text.Trim().IndexOf(" ") > 0 ) {
                    names = customer.Text.Trim().Split();
                }else {
                    names[1] = customer.Text.Trim();
                }

                custResults.Items.Clear();
                foreach (Customer cust in DAL.getCustomersByFullName(names[0], names[1]))
                {
                    custResults.Items.Add(cust);
                }
				
            };
			
			// CustInfoButton
        	this.custInfoButton.Location = new Point (325, 550);
        	this.custInfoButton.Name = "CustInfoButton";
        	this.custInfoButton.TabIndex = 4;
        	this.custInfoButton.Text = "Info";
			this.custInfoButton.Enabled = false;
        	this.custInfoButton.Click += delegate(object sender, EventArgs e) {
                foreach( Customer sel in custResults.SelectedItems){
					infoPanel("Customer", sel); 
				}
            };
			
			// CustCheckIO
			this.custCheckIO.Location = new Point (400, 550);
			this.custCheckIO.Name = "CustInfoButton";
			this.custCheckIO.TabIndex = 4;
			this.custCheckIO.Text = "Check";
			this.custCheckIO.Enabled = false;
			this.custCheckIO.Click += delegate(object sender, EventArgs e) {
				Form dialog = new Form();				
				Label message = new Label();
				Button accept = new Button();
								
				message.Text = this.selectedMovie.Text.Substring( 0, this.selectedMovie.Text.IndexOf(','));
				
				message.Location = new Point( 65, 5 );
				message.Size = new Size(  100, 32);
				
				accept.Text = "Ok";
				accept.Location = new Point( 55, 45 );
				accept.Click += delegate {
					dialog.Dispose();
				};
				
				dialog.Size = new Size (180, 100);
				dialog.Text = "Success!";
				dialog.Controls.Add (accept);
				dialog.Controls.Add (message);
				dialog.Visible = true;
			};
			
			
        	this.addNewCust.Location = new Point( 475, 550);
			this.addNewCust.Name = "AddNewCust";
			this.addNewCust.Text = "Add New Customer";
			this.addNewCust.Click += delegate(object sender, EventArgs e) {
				infoPanel( "Customer", null);
			};
			
        	CusInfo.Controls.Add (this.customerHeader);
			CusInfo.Controls.Add (this.selectedMovie);
        	CusInfo.Controls.Add (this.address);
        	CusInfo.Controls.Add (this.customer);
        	CusInfo.Controls.Add (this.cardNum);
        	CusInfo.Controls.Add (this.custResults);
        	CusInfo.Controls.Add (this.custSubmit);
			CusInfo.Controls.Add (this.custInfoButton);
			CusInfo.Controls.Add (this.custCheckIO);
			CusInfo.Controls.Add( this.addNewCust);
            return CusInfo;
        }


        public void infoPanel (String title, Object selection)
        {

            char[] delim = new char[ 2];
			delim[ 0 ] = ',';  // commas and spaces
			delim[ 1 ] = ' ';
			
			this.info = new Form ();
        	this.info.ClientSize = new Size (500, 610);
        	this.info.Text = title;
    
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
   
			Label L6 = new Label ();
        	L6.Location = new Point (30, 300);
        	L6.Size = new Size (60, 32);
        	TextBox T6 = new TextBox ();
        	T6.Size = new Size (335, 32);
        	T6.Location = new Point (90, 300);
			
			ListBox details = new ListBox ();
        	details.Location = new Point (30, 350);
        	details.Size = new Size (375, 250);
			
			if (title.ToLower ().Equals ("movie")) 
			{
                Movie data = (Movie)selection;

        		L1.Text = "Title";
        		info.Controls.Add (L1);
        		T1.Text = selection == null ? "Enter Title Here" : data.Title;
        		info.Controls.Add (T1);

                L2.Text = "StoreId";
        		info.Controls.Add (L2);
        		info.Controls.Add (T2);
   
				L3.Text = "Release Date (DD/MM/YYYY";
                T3.Text = selection == null ? "Enter Date Here" : data.DistroDate.ToString();
        		info.Controls.Add (L3);
        		info.Controls.Add (T3);
    
				L4.Text = "Genre";
                T4.Text = selection == null ? "Enter Genres Here" : data.GenreString;
        		info.Controls.Add (L4);
        		info.Controls.Add (T4);
				
				L5.Text = "Cast/Crew";
                T5.Text = selection == null ? "Enter Cast/Crew Here" : data.CastCrewString;
				info.Controls.Add (L5);
				info.Controls.Add (T5);
				
				L6.Text = "Publisher";
				info.Controls.Add( L6);
				info.Controls.Add (T6);
			} else if( title.ToLower().Equals( "customer" )){

                Customer data = (Customer)selection;

				L1.Text = "Name (Last/First)";
                T1.Text = selection == null ? "Enter Name Here" : data.FirstName + " " + data.LastName;
				info.Controls.Add (L1);
				info.Controls.Add (T1);
				
				L2.Text = "Address";
                T2.Text = selection == null ? "Enter Address Here" : data.BillAddress.ToString();
				info.Controls.Add (L2);
				info.Controls.Add (T2);
				
				L3.Text = "Card Number";
                T3.Text = selection == null ? "Enter Card Number Here" : data.CardNumber.ToString();
				info.Controls.Add (L3);
				info.Controls.Add (T3);

				L4.Text = "Card Expiration Date";
                T4.Text = selection == null ? "Enter expiration date here" : data.ExpDate.ToString();
				info.Controls.Add( L4 );
				info.Controls.Add( T4 );
			}
			
			Button addItem = new Button();
			addItem.Location = new Point( 420, 450);
            addItem.Text = selection == null ? "Add" : "Edit";			
			addItem.Click +=  delegate(object sender, EventArgs e) {
				if( title.ToLower().Equals( "movie") ){

                    String movieTitle = T1.Text.Trim();
                    DateTime releaseDate = DateTime.Parse(T3.Text.Trim());
                    String genreString = T4.Text.Trim();
                    int id = -1;

                    Movie newMov;

                    if (selection == null)
                    {
                        newMov = new Movie(id, movieTitle, releaseDate, genreString);
                    }
                    else
                    {
                        newMov = (Movie)selection;
                        newMov.Title = newMov.Title.CompareTo(movieTitle) == 0 ? newMov.Title : movieTitle;
                        newMov.DistroDate = newMov.DistroDate.CompareTo(releaseDate) == 0 ?
                            newMov.DistroDate : releaseDate;
                        newMov.GenreString = newMov.GenreString.CompareTo(genreString) == 0 ?
                            newMov.GenreString : genreString;
                    }

					DAL.insertMovie(newMov);
				} else if( title.ToLower().Equals("customer")) {

                    String[] names = { "", "" };
                    if (customer.Text.Trim().IndexOf(" ") > 0)
                    {
                        names = customer.Text.Trim().Split();
                    }
                    else
                    {
                        names[1] = customer.Text.Trim();
                    }

                    Address addr = Address.Parse(T2.Text.Trim());
                    String cardNo = T3.Text.Trim();
                    DateTime cardExp = DateTime.Parse(T4.Text.Trim());

                    Customer newCust;

                    if (selection == null)
                    {
                        Person newPerson = new Person(-1, names[0], names[1]);
                        newCust = new Customer(newPerson, cardNo, cardExp, addr);
                    }
                    else
                    {
                        newCust = (Customer)selection;
                        newCust.FirstName = names[0];
                        newCust.LastName = names[1];
                        newCust.CardNumber = cardNo;
                        newCust.ExpDate = cardExp;
                        newCust.BillAddress = addr;
                    }


                    DAL.insertCustomer(newCust);
				}
				
			};
			this.info.Controls.Add( addItem );
			this.info.Controls.Add( details);			
			this.info.Visible = true;
		}
    }
}