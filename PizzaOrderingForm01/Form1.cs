using MySql.Data.MySqlClient;
using System;
using System.Management;
using System.Windows.Forms;
namespace PizzaOrderingForm01
{
    public partial class Form1 : Form
    {
        public MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd;

        public Form1()
        {
            InitializeComponent();
            con.ConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=pizza_db;";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = ""; // Define the query based on selected size

            if (comboBox1.SelectedItem.ToString() == "Small")
            {
                query = "SELECT Prices FROM pizza_prices WHERE ID = 1";
            }
            else if (comboBox1.SelectedItem.ToString() == "Medium")
            {
                query = "SELECT Prices FROM pizza_prices WHERE ID = 2";
            }
            else if (comboBox1.SelectedItem.ToString() == "Large")
            {
                query = "SELECT Prices FROM pizza_prices WHERE ID = 3";
            }
            else
            {
                price_box1.Text = "0";
                return; // Exit if no valid size is selected
            }

            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                con.Open();
                try
                {
                    Int32 fun = Convert.ToInt32(cmd.ExecuteScalar());
                    price_box1.Text = fun.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            con.Close(); // Close the connection
        }

        private void quantityB1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(price_box1.Text, out int price) && int.TryParse(quantityB1.Text, out int quantity))
            {
                TotalB1.Text = (price * quantity).ToString();
            }
            else
            {
                TotalB1.Text = ""; // Clear the total value if the input is not valid
            }

            // Clear the other text boxes
         
        }


        private void button4_Click(object sender, EventArgs e)
        {
            //this function show the sub total
            //the code for the add data to list view
            if (TotalB1.Text.Length > 0)
            {
                string[] arr = new string[4];
                arr[0] = comboBox1.Text;
                arr[1] = price_box1.Text;
                arr[2] = quantityB1.Text;
                arr[3] = TotalB1.Text;
                ListViewItem item = new ListViewItem(arr);
                listView1.Items.Add(item);
                //this function show the sub total

                int x = int.Parse(quantityB1.Text);
                int y = int.Parse(price_box1.Text);


                subTotal.Text = ( x * y ).ToString();

            }
            price_box1.Text = "";
            quantityB1.Text = "";
        }
    }
    }

