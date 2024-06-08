using BusinessObjects;
using Services;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Collections.Generic;

namespace WPFApp
{
    public partial class MainWindow : Window
    {
        private readonly IProductService iProductService; 
        private readonly ICategoryService iCategoryService;
        private static int productId = -1;
              
        public MainWindow()
        {
            InitializeComponent();
            iProductService = new ProductService();
            iCategoryService = new CategoryService();
            productId = iProductService.GetProducts().Last().ProductId + 1; 
        }

        public void LoadCategoryList()
        {
            try
            {
                var catList = iCategoryService.GetCategory();
                cboCategory.ItemsSource = catList;
                cboCategory.DisplayMemberPath = "CategoryName";
                cboCategory.SelectedValuePath = "CategoryId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of categories"); 
            }
        }

        public void LoadProductList()
        {
            try
            {
                var productList = iProductService.GetProducts();
                dgData.ItemsSource = null;//Clear old dgData
                dgData.ItemsSource = productList;
            }
            catch (Exception ex)
            {
//                MessageBox.Show(ex.Message, "Error on load list of products"); 
            }
            finally
            {
                resetInput();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategoryList();
            LoadProductList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = new Product();
                product.ProductId = productId++; 
                product.ProductName = txtProductName.Text;
                product.UnitPrice = decimal.Parse(txtPrice.Text);
                product.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                product.CategoryId = int.Parse(cboCategory.SelectedValue.ToString()); 
                iProductService.SaveProduct(product); 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
            finally
            {
                LoadProductList(); 
            }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                DataGrid dataGrid = sender as DataGrid;
                DataGridRow row =
                    (DataGridRow)dataGrid.ItemContainerGenerator
                    .ContainerFromIndex(dataGrid.SelectedIndex);
                DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                string id = ((TextBlock)RowColumn.Content).Text;
                Product product = iProductService.GetProductById(Int32.Parse(id));
                txtProductID.Text = product.ProductId.ToString();
                txtProductName.Text = product.ProductName;
                txtPrice.Text = product.UnitPrice.ToString();
                txtUnitsInStock.Text = product.UnitsInStock.ToString();
                cboCategory.SelectedValue = product.CategoryId;
            }
            catch (Exception ex)//handle exception cause by extracting id from the selected row
            {

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(txtProductID.Text, out int productId))
                {
                    Product product = new Product();
                    product.ProductId = productId;
                    product.ProductName = txtProductName.Text;
                    product.UnitPrice = Decimal.Parse(txtPrice.Text);
                    product.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                    product.CategoryId = Int32.Parse(cboCategory.SelectedValue.ToString());
                    iProductService.UpdateProduct(product); 
                }
                else
                {
                    MessageBox.Show("You must select a product!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
            finally
            {
                LoadProductList(); 
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(txtProductID.Text, out int productId))
                {
                    Product product = new Product { ProductId = productId };
                    iProductService.DeleteProduct(product); 
                }
                else
                {
                    MessageBox.Show("You must select a Product!"); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error"); 
            }
            finally
            {
                LoadProductList();
            }
        }

        private void resetInput()
        {
            txtProductID.Text = "";
            txtProductName.Text = "";
            txtPrice.Text = "";
            txtUnitsInStock.Text = "";
            cboCategory.SelectedValue = 0;
        }
    }
}
