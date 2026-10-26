using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using FirebirdSql.Data.FirebirdClient;
using System.Data;
using WpfApplication.Modells;

namespace WpfApplication
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Product> products = new ObservableCollection<Product>();
        ObservableCollection<Bill> orderProducts = new ObservableCollection<Bill>();
        private const string ConnectionString =
        "User=SYSDBA;Password=masterkey;" +
        "Database=/home/ubuntu/BASE_Denis.FDB;" +
        "DataSource=13.63.178.6;Port=3050;Dialect=3;Charset=UTF8;" +
        "authPlugins=Srp256,Srp,Legacy_Auth;";
        public MainWindow()
        {
            InitializeComponent();
            DownloadListProduct();
            ListOrder.ItemsSource = orderProducts;
        }
        private void AddMivina(object sender, RoutedEventArgs e)
        {
            string count = TxtCount.Text;
            string city = (CmbCity.SelectedItem as ComboBoxItem)?.Content.ToString();
            /* products.Add(
             new Product
             {
                 Name = "Mivina",
                 Price = 10,
                 Quantity = TxtCount.Text
             }
         );*/

            MessageBox.Show(count + " Mivina added! " + city);

        }

        private void AddGolubzi(object sender, RoutedEventArgs e)
        {
            string count = TxtCount.Text;
            string city = (CmbCity.SelectedItem as ComboBoxItem)?.Content.ToString();
            ListOrder.Items.Add("Golubzi: " + count + " in " + city);
            MessageBox.Show(count + " Golubzi added! " + city);
        }

        private void AddCrousan(object sender, RoutedEventArgs e)
        {
            string count = TxtCount.Text;
            string city = (CmbCity.SelectedItem as ComboBoxItem)?.Content.ToString();
            /*products.Add(
           new Product
           {
               Name = "",
               Price = 14,
               Quantity = TxtCount.Text
           }
              );


            MessageBox.Show(count + " Crousan added! " + city);*/
        }

        private void AddCoffee3in1(object sender, RoutedEventArgs e)
        {
            string count = TxtCount.Text;
            string city = (CmbCity.SelectedItem as ComboBoxItem)?.Content.ToString();
            /*products.Add(
            new Product
           {
               Name = "Coffee 3 in 1",
               Price = 3,
               Quantity = TxtCount.Text
           }
           );


            MessageBox.Show("Bro, you realy know what is 3 in 1? How old are you? " + count + " This Coffe will be Free! " + city); */
        }

        private void AddTea(object sender, RoutedEventArgs e)
        {
            string count = TxtCount.Text;
            string city = (CmbCity.SelectedItem as ComboBoxItem)?.Content.ToString();
            /*products.Add(
           new Product
           {
               Name = "Tea",
               Price = 5,
               Quantity = TxtCount.Text
           }
           );


            MessageBox.Show(count + " Tea added! " + city); */
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ListOrder.Items.Clear();
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {

        }
        public void DownloadListProduct()
        {
            FbConnection connection = new FbConnection(ConnectionString);
            connection.Open();
            string sql = "SELECT * FROM T_PRODUCT";
            FbCommand cmd = new FbCommand(sql, connection);
            FbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string id = reader["ID_THIS_PRODUCT"].ToString();
                string name = reader["NAME_PRODUCT"].ToString();
                string weight = reader["WEIGHT_THIS_PRODUCT"].ToString();
                string price = reader["PRICE_PRODUCT"].ToString();
                string available = reader["AVAILABLE_PRODUCT"].ToString();
                Button button = new Button();
                button.Width = 150;
                button.Height = 100;
                button.Margin = new Thickness(5);
                button.Content =
                    $"Name: {name}\n" +
                    $"Weight: {weight}\n" +
                    $"Price: {price}\n" +
                    $"Available: {available}\n";
                button.Tag = id;
                button.Click += Product_Click;
                ListProducts.Children.Add(button);


                products.Add(
                new Product
                {
                    ProductId = int.Parse(id),
                    ProductName = name,
                    ProductWeight = float.Parse(weight),
                    ProductPrice = float.Parse(price),
                    ProductAvailable = available == "1" ? true : false
                });
            }
            connection.Close();
        }
        private void Product_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int idDish = Convert.ToInt32(button.Tag);
            Product this_product = GetProductById(idDish);
            if (this_product != null)
            {
                MessageBox.Show("You clicked on product with ID: " + idDish + "\nName: " + this_product.ProductName);
            }
            foreach (var item in orderProducts)
            {
                if (item.idProduct == idDish)
                {
                    item.CountProduct += int.Parse(TxtCount.Text);
                    return;
                }
            }
            orderProducts.Add(new Bill
            {
                idProduct = this_product.ProductId,
                countProduct = int.Parse(TxtCount.Text),
                nameProduct = this_product.ProductName,
                DateWrite = DateTime.UtcNow
            });
        }
        public Product GetProductById(int id)
        {
            foreach (var product in products)
            {
                if (product.ProductId == id)
                {
                    return product;
                }
            }
            return null;
        }

    }
 


}