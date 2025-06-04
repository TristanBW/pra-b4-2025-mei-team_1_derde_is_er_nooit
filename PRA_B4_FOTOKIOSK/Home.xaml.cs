using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace PRA_B4_FOTOKIOSK
{
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();

            ShopManager.Instance = this;

            // Vul de productenlijst
            ShopManager.Products.Add(new KioskProduct { Name = "Foto afdruk 10x15", Price = 0.50m });
            ShopManager.Products.Add(new KioskProduct { Name = "Foto afdruk 13x18", Price = 1.00m });
            ShopManager.Products.Add(new KioskProduct { Name = "Fotoboek", Price = 15.00m });
            ShopManager.Products.Add(new KioskProduct { Name = "Canvas print", Price = 25.00m });
            ShopManager.Products.Add(new KioskProduct { Name = "Mok met foto", Price = 8.50m });
            ShopManager.Products.Add(new KioskProduct { Name = "T-shirt met foto", Price = 12.99m });

            ShopManager.UpdateDropDownProducts();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Refresh button clicked!");
        }

        private void btnShopAdd_Click(object sender, RoutedEventArgs e)
        {
            ShopManager.AddToReceipt();
        }

        private void btnShopReset_Click(object sender, RoutedEventArgs e)
        {
            tbFotoId.Text = "";
            tbAmount.Text = "";
            cbProducts.SelectedIndex = -1;
            ShopManager.SetShopReceipt("");
            ShopManager.ResetTotaalBedrag();
            lblTotalAmount.Content = "Totaalbedrag: €0,00";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Haal de bon op via ShopManager
            string bon = ShopManager.GetShopReceipt();

            // Controleer of er iets is om op te slaan
            if (string.IsNullOrWhiteSpace(bon))
            {
                MessageBox.Show("Er is geen bon om op te slaan.");
                return;
            }

            // Bepaal het pad waar het bestand wordt opgeslagen: in de Documentenmap
            string bestandsPad = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "bon.txt");

            try
            {
                // Schrijf de bon naar het tekstbestand
                File.WriteAllText(bestandsPad, bon);
                MessageBox.Show($"Bon succesvol opgeslagen in:\n{bestandsPad}");
            }
            catch (IOException ex)
            {
                // Toon een foutmelding als het opslaan mislukt
                MessageBox.Show("Fout bij opslaan van de bon:\n" + ex.Message);
            }
        }

        private void btnZoeken_Click(object sender, RoutedEventArgs e)
        {
            string zoekterm = tbZoeken.Text.Trim();
            if (string.IsNullOrEmpty(zoekterm))
            {
                MessageBox.Show("Vul een zoekterm in.");
                return;
            }

            lbSearchInfo.Content = $"Zoeken naar: {zoekterm}";

            // Eventueel hier productzoekfunctionaliteit implementeren
            // Bijvoorbeeld producten filteren op naam of categorie
        }
    }
}
