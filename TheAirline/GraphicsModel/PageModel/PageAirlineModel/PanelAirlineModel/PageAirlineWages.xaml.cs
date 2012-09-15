﻿using System;
using System.Collections.Generic;
using System.Linq;
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
using TheAirline.Model.AirlineModel;
using TheAirline.Model.GeneralModel;
using TheAirline.GraphicsModel.PageModel.GeneralModel;
using TheAirline.GraphicsModel.UserControlModel.MessageBoxModel;
using TheAirline.GraphicsModel.UserControlModel;
using TheAirline.Model.AirportModel;
using TheAirline.Model.AirlinerModel;
using TheAirline.GraphicsModel.Converters;
using TheAirline.Model.AirlinerModel.RouteModel;

namespace TheAirline.GraphicsModel.PageModel.PageAirlineModel.PanelAirlineModel
{
    /// <summary>
    /// Interaction logic for PageAirlineWages.xaml
    /// </summary>
    public partial class PageAirlineWages : Page
    {
        private Airline Airline;
        private StackPanel panelWages, panelEmployees, panelInflightServices;
        private Dictionary<FeeType, double> FeeValues;
        private ListBox lbWages, lbFees, lbFoodDrinks;
        public PageAirlineWages(Airline airline)
        {
            InitializeComponent();

            this.Airline = airline;

            this.FeeValues = new Dictionary<FeeType, double>();

            FeeTypes.GetTypes().ForEach(f => this.FeeValues.Add(f, this.Airline.Fees.getValue(f)));

            StackPanel panelWagesAndEmployees = new StackPanel();
          
            WrapPanel panelMenuButtons = new WrapPanel();
            panelWagesAndEmployees.Children.Add(panelMenuButtons);

            ucSelectButton sbWages = new ucSelectButton();
            sbWages.Uid = "1004";
            sbWages.Content = Translator.GetInstance().GetString("PageAirlineWages", sbWages.Uid);
            sbWages.IsSelected = true;
            sbWages.Click += new RoutedEventHandler(sbWages_Click);
            panelMenuButtons.Children.Add(sbWages);

            ucSelectButton sbEmployees = new ucSelectButton();
            sbEmployees.Uid = "1005";
            sbEmployees.Content = Translator.GetInstance().GetString("PageAirlineWages", sbEmployees.Uid);
            sbEmployees.Click += new RoutedEventHandler(sbEmployees_Click);
            panelMenuButtons.Children.Add(sbEmployees);

            ucSelectButton sbService = new ucSelectButton();
            sbService.Uid = "1006";
            sbService.Content = Translator.GetInstance().GetString("PageAirlineWages", sbService.Uid);
            sbService.Click += new RoutedEventHandler(sbService_Click);
            panelMenuButtons.Children.Add(sbService);

            panelWages = createWagesPanel();
            panelWagesAndEmployees.Children.Add(panelWages);

            panelEmployees = createEmployeesPanel();
            panelEmployees.Visibility = System.Windows.Visibility.Collapsed;
            panelWagesAndEmployees.Children.Add(panelEmployees);

            panelInflightServices = createInflightServicesPanel();
            panelInflightServices.Visibility = System.Windows.Visibility.Collapsed;
            panelWagesAndEmployees.Children.Add(panelInflightServices);


     
            this.Content = panelWagesAndEmployees;
        }
        //creates the inflight services panel
        private StackPanel createInflightServicesPanel()
        {
            StackPanel panelServices = new StackPanel();

            TextBlock txtServicesHeader = new TextBlock();
            txtServicesHeader.Uid = "1007";
            txtServicesHeader.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            txtServicesHeader.FontWeight = FontWeights.Bold;
            txtServicesHeader.SetResourceReference(TextBlock.BackgroundProperty, "HeaderBackgroundBrush2");
            txtServicesHeader.Text = Translator.GetInstance().GetString("PageAirlineWages", txtServicesHeader.Uid);

            panelServices.Children.Add(txtServicesHeader);

            foreach (AirlinerClass.ClassType type in Enum.GetValues(typeof(AirlinerClass.ClassType)))
            {
                TextBlock txtClassHeader = new TextBlock();
                txtClassHeader.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                txtClassHeader.FontWeight = FontWeights.Bold;
                txtClassHeader.SetResourceReference(TextBlock.BackgroundProperty, "HeaderBackgroundBrush2");
                txtClassHeader.Text = new TextUnderscoreConverter().Convert(type).ToString();
                txtClassHeader.Margin = new Thickness(0, 5, 0, 0);

                panelServices.Children.Add(txtClassHeader);
    
                ListBox lbServices = new ListBox();
                lbServices.ItemContainerStyleSelector = new ListBoxItemStyleSelector();
                lbServices.SetResourceReference(ListBox.ItemTemplateProperty, "QuickInfoItem");

                panelServices.Children.Add(lbServices);

                foreach (RouteFacility.FacilityType facilityType in Enum.GetValues(typeof(RouteFacility.FacilityType)))
                {
                    ComboBox cbFacility = new ComboBox();
                    cbFacility.SetResourceReference(ComboBox.StyleProperty, "ComboBoxTransparentStyle");
                    cbFacility.Width = 200;
                    cbFacility.DisplayMemberPath = "Name";
                    cbFacility.SelectedValuePath = "Name";

                    RouteFacilities.GetFacilities(facilityType).ForEach(f => cbFacility.Items.Add(f));

                    lbServices.Items.Add(new QuickInfoValue(facilityType.ToString(), cbFacility));


                }

             }



            return panelServices;
        }
      
        //creates the employees panel
        private StackPanel createEmployeesPanel()
        {
            StackPanel panelEmployees = new StackPanel();

            TextBlock txtHeaderWages = new TextBlock();
            txtHeaderWages.Uid = "1001";
            txtHeaderWages.Margin = new Thickness(0, 0, 0, 0);
            txtHeaderWages.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            txtHeaderWages.SetResourceReference(TextBlock.BackgroundProperty, "HeaderBackgroundBrush2");
            txtHeaderWages.FontWeight = FontWeights.Bold;
            txtHeaderWages.Text = Translator.GetInstance().GetString("PageAirlineWages", txtHeaderWages.Uid);

            panelEmployees.Children.Add(txtHeaderWages);

            lbWages = new ListBox();
            lbWages.ItemContainerStyleSelector = new ListBoxItemStyleSelector();
            lbWages.SetResourceReference(ListBox.ItemTemplateProperty, "QuickInfoItem");

            foreach (FeeType type in FeeTypes.GetTypes(FeeType.eFeeType.Wage))
                lbWages.Items.Add(new QuickInfoValue(type.Name, createWageSlider(type)));
            panelEmployees.Children.Add(lbWages);

            TextBlock txtEmployeesHeader = new TextBlock();
            txtEmployeesHeader.Uid = "1005";
            txtEmployeesHeader.Margin = new Thickness(0, 5, 0, 0);
            txtEmployeesHeader.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            txtEmployeesHeader.SetResourceReference(TextBlock.BackgroundProperty, "HeaderBackgroundBrush2");
            txtEmployeesHeader.FontWeight = FontWeights.Bold;
            txtEmployeesHeader.Text = Translator.GetInstance().GetString("PageAirlineWages", txtEmployeesHeader.Uid);

            panelEmployees.Children.Add(txtEmployeesHeader);

            ListBox lbEmployees = new ListBox();
            lbEmployees.ItemContainerStyleSelector = new ListBoxItemStyleSelector();
            lbEmployees.ItemTemplate = this.Resources["EmployeeItem"] as DataTemplate;

            int cockpitCrew = this.Airline.Fleet.Sum(f => f.Airliner.Type.CockpitCrew);

            var list = (from r in this.Airline.Fleet.SelectMany(f => f.Routes) select r);

            int cabinCrew = this.Airline.Routes.Sum(r => r.getTotalCabinCrew());

            int serviceCrew = this.Airline.Airports.SelectMany(a=>a.getCurrentAirportFacilities(this.Airline)).Where(a=>a.EmployeeType==AirportFacility.EmployeeTypes.Support).Sum(a=>a.NumberOfEmployees);
            int maintenanceCrew = this.Airline.Airports.SelectMany(a => a.getCurrentAirportFacilities(this.Airline)).Where(a=>a.EmployeeType==AirportFacility.EmployeeTypes.Maintenance).Sum(a => a.NumberOfEmployees);

            lbEmployees.Items.Add(new KeyValuePair<string, int>("Cockpit crew", cockpitCrew));
            lbEmployees.Items.Add(new KeyValuePair<string, int>("Cabin crew", cabinCrew));
            lbEmployees.Items.Add(new KeyValuePair<string, int>("Support crew", serviceCrew));
            lbEmployees.Items.Add(new KeyValuePair<string, int>("Maintenance crew", maintenanceCrew));            

            panelEmployees.Children.Add(lbEmployees);

            panelEmployees.Children.Add(createButtonsPanel());

            return panelEmployees;
        }
        //creates the wage panel
        private StackPanel createWagesPanel()
        {
            StackPanel panelWagesFee = new StackPanel();

            TextBlock txtHeaderFoods = new TextBlock();
            txtHeaderFoods.Uid = "1002";
            //txtHeaderFoods.Margin = new Thickness(0, 5, 0, 0);
            txtHeaderFoods.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            txtHeaderFoods.SetResourceReference(TextBlock.BackgroundProperty, "HeaderBackgroundBrush2");
            txtHeaderFoods.FontWeight = FontWeights.Bold;
            txtHeaderFoods.Text = Translator.GetInstance().GetString("PageAirlineWages", txtHeaderFoods.Uid);

            panelWagesFee.Children.Add(txtHeaderFoods);

            lbFoodDrinks = new ListBox();
            lbFoodDrinks.ItemContainerStyleSelector = new ListBoxItemStyleSelector();
            lbFoodDrinks.SetResourceReference(ListBox.ItemTemplateProperty, "QuickInfoItem");

            foreach (FeeType type in FeeTypes.GetTypes(FeeType.eFeeType.FoodDrinks))
                lbFoodDrinks.Items.Add(new QuickInfoValue(type.Name, createWageSlider(type)));

            panelWagesFee.Children.Add(lbFoodDrinks);

            TextBlock txtHeaderFees = new TextBlock();
            txtHeaderFees.Uid = "1003";
            txtHeaderFees.Margin = new Thickness(0, 5, 0, 0);
            txtHeaderFees.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            txtHeaderFees.SetResourceReference(TextBlock.BackgroundProperty, "HeaderBackgroundBrush2");
            txtHeaderFees.FontWeight = FontWeights.Bold;
            txtHeaderFees.Text = Translator.GetInstance().GetString("PageAirlineWages", txtHeaderFees.Uid);

            panelWagesFee.Children.Add(txtHeaderFees);

            lbFees = new ListBox();
            lbFees.ItemContainerStyleSelector = new ListBoxItemStyleSelector();
            lbFees.SetResourceReference(ListBox.ItemTemplateProperty, "QuickInfoItem");

            foreach (FeeType type in FeeTypes.GetTypes(FeeType.eFeeType.Fee))
                lbFees.Items.Add(new QuickInfoValue(type.Name, createWageSlider(type)));

            panelWagesFee.Children.Add(lbFees);
            panelWagesFee.Children.Add(createButtonsPanel());

            return panelWagesFee;
        }
        private void sbEmployees_Click(object sender, RoutedEventArgs e)
        {
            panelWages.Visibility = System.Windows.Visibility.Collapsed;
            panelEmployees.Visibility = System.Windows.Visibility.Visible;
            panelInflightServices.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void sbWages_Click(object sender, RoutedEventArgs e)
        {
            panelWages.Visibility = System.Windows.Visibility.Visible;
            panelEmployees.Visibility = System.Windows.Visibility.Collapsed;
            panelInflightServices.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void sbService_Click(object sender, RoutedEventArgs e)
        {
            panelWages.Visibility = System.Windows.Visibility.Collapsed;
            panelEmployees.Visibility = System.Windows.Visibility.Collapsed;
            panelInflightServices.Visibility = System.Windows.Visibility.Visible;
        }
        //creates the buttons panel
        private WrapPanel createButtonsPanel()
        {
            WrapPanel buttonsPanel = new WrapPanel();
            buttonsPanel.Margin = new Thickness(0, 5, 0, 0);

            Button btnOk = new Button();
            btnOk.Uid = "100"; 
            btnOk.SetResourceReference(Button.StyleProperty, "RoundedButton");
            btnOk.Height = Double.NaN;
            btnOk.Width = Double.NaN;
            btnOk.Content = Translator.GetInstance().GetString("General", btnOk.Uid);
            btnOk.Click += new RoutedEventHandler(btnOk_Click);
            btnOk.SetResourceReference(Button.BackgroundProperty, "ButtonBrush");

            buttonsPanel.Children.Add(btnOk);

            Button btnUndo = new Button();
            btnUndo.Uid = "103";
            btnUndo.SetResourceReference(Button.StyleProperty, "RoundedButton");
            btnUndo.Height = Double.NaN;
            btnUndo.Margin = new Thickness(5, 0, 0, 0);
            btnUndo.Width = Double.NaN;
            btnUndo.Click += new RoutedEventHandler(btnUndo_Click);
            btnUndo.Content = Translator.GetInstance().GetString("General", btnUndo.Uid);
            btnUndo.SetResourceReference(Button.BackgroundProperty, "ButtonBrush");

            buttonsPanel.Children.Add(btnUndo);

            return buttonsPanel;
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            lbWages.Items.Clear();
            foreach (FeeType type in FeeTypes.GetTypes(FeeType.eFeeType.Wage))
            {
                this.FeeValues[type] = this.Airline.Fees.getValue(type);
                lbWages.Items.Add(new QuickInfoValue(type.Name, createWageSlider(type)));
            }

            lbFees.Items.Clear();
            foreach (FeeType type in FeeTypes.GetTypes(FeeType.eFeeType.Fee))
            {
                this.FeeValues[type] = this.Airline.Fees.getValue(type);
                lbFees.Items.Add(new QuickInfoValue(type.Name, createWageSlider(type)));
            }
            lbFoodDrinks.Items.Clear();
            foreach (FeeType type in FeeTypes.GetTypes(FeeType.eFeeType.FoodDrinks))
            {
                this.FeeValues[type] = this.Airline.Fees.getValue(type);
                lbFoodDrinks.Items.Add(new QuickInfoValue(type.Name, createWageSlider(type)));
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            WPFMessageBoxResult result = WPFMessageBox.Show(Translator.GetInstance().GetString("MessageBox", "2108"), Translator.GetInstance().GetString("MessageBox", "2108", "message"), WPFMessageBoxButtons.YesNo);
            if (result == WPFMessageBoxResult.Yes)
            {
                foreach (FeeType type in this.FeeValues.Keys)
                    this.Airline.Fees.setValue(type, this.FeeValues[type]);
            }
        }

        //creates the slider for a wage type
        private WrapPanel createWageSlider(FeeType type)
        {
            WrapPanel sliderPanel = new WrapPanel();

            TextBlock txtValue = UICreator.CreateTextBlock(string.Format("{0:C}", this.FeeValues[type]));
            txtValue.VerticalAlignment = VerticalAlignment.Bottom;
            txtValue.Margin = new Thickness(5, 0, 0, 0);
            txtValue.Tag = type;

            Slider slider = new Slider();
            slider.Width = 200;
            slider.Value = this.FeeValues[type];
            slider.Tag = txtValue;
            slider.Maximum = type.MaxValue;
            slider.Minimum = type.MinValue;
            slider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(slider_ValueChanged);
            slider.TickFrequency = (type.MaxValue - type.MinValue) / slider.Width;
            slider.IsSnapToTickEnabled = true;
            slider.IsMoveToPointEnabled = true;
            sliderPanel.Children.Add(slider);

            sliderPanel.Children.Add(txtValue);

            return sliderPanel;
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            TextBlock txtBlock = (TextBlock)slider.Tag;
            txtBlock.Text = string.Format("{0:C}", slider.Value);

            FeeType type = (FeeType)txtBlock.Tag;

            this.FeeValues[type] = slider.Value;
        }
    }
}
