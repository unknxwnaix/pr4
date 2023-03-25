using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int finalsum = 0; 
        List<zametka> list = new List<zametka>();
        public MainWindow()
        {
            InitializeComponent();
            Dtp.SelectedDate = DateTime.Now;
            Dtp.DisplayDateStart = new DateTime(2023, 01, 01);
            Dtp.DisplayDateEnd = new DateTime(2023, 12, 31);
            dgrid.ItemsSource = list;
            dgrid.ItemsSource = null;
            list = serialize.Deserialize<List<zametka>>();
            List<zametka> listd = new List<zametka>();
            int a = 0;
            foreach (var item in list)
            {
                if (Dtp.Text == item.Date)
                {
                    listd.Add(list[a]);

                }
                finalsum += Convert.ToInt32(item.Sum);
                a++;
            }
            dgrid.ItemsSource = listd;
            finalsumw();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> listt = new List<string>();
            foreach (var item in cbox.Items) 
            {
                listt.Add(Convert.ToString(item));
            }
            Добавить_тип new_type = new Добавить_тип();
            var result = new_type.ShowDialog();
            if (result == true && new_type.typeBox.Text != null && new_type.typeBox.Text != "")
            {
                string txt = new_type.typeBox.Text;
                listt.Add(txt);
            }
            cbox.ItemsSource = listt;
        }

        private void new_z_Click(object sender, RoutedEventArgs e)
        {
            bool isplus;
            if(Convert.ToInt32(moneybox.Text) > 0)
            {
                isplus = true;
            }
            else
            {
                isplus = false;
            }
            finalsum += Convert.ToInt32(moneybox.Text);
            string datee = Dtp.Text;
            zametka zametka = new zametka(namebox.Text, Convert.ToString(cbox.SelectedItem), moneybox.Text, isplus, datee);
            list.Add(zametka);
            dgrid.ItemsSource = null;
            List<zametka> listd = new List<zametka>();
            int a = 0;
            foreach (var item in list)
            {
                if (Dtp.Text == item.Date)
                {
                    listd.Add(list[a]);
                }
                a++;
            }
            dgrid.ItemsSource = null;
            dgrid.ItemsSource = listd;
            finalsumw();
        }

        private void changeZ_Click(object sender, RoutedEventArgs e)
        {
            bool isplus;
            if (Convert.ToInt32(moneybox.Text) > 0)
            {
                isplus = true;
            }
            else
            {
                isplus = false;
            }
            zametka selected = dgrid.SelectedItem as zametka;
            if (selected != null)
            {
                finalsum -= Convert.ToInt32(selected.Sum);
                finalsum += Convert.ToInt32(moneybox.Text);
                zametka zametka = new zametka(namebox.Text, cbox.Text, moneybox.Text, isplus, Dtp.Text);
                /*list[dgrid.SelectedIndex] = zametka;*/
                int c = 0;
                List<zametka> listchange = new List<zametka>();
                foreach (var item in list)
                {
                    if(item == selected)
                    {
                        listchange.Add(zametka);
                    }
                    else
                    {
                        listchange.Add(item);
                    }
                    c++;
                }
                list.Clear();
                list = listchange;
                List<zametka> listd = new List<zametka>();
                int a = 0;
                foreach (var item in list)
                {
                    if (Dtp.Text == Convert.ToString(item.Date))
                    {
                        listd.Add(list[a]);
                    }
                    a++;
                }
                dgrid.ItemsSource = null;
                dgrid.ItemsSource = listd;
                finalsumw();
                
            }
        }

        private void deleteZ_Click(object sender, RoutedEventArgs e)
        {
            zametka selected = dgrid.SelectedItem as zametka;
            int c = 0;
            int deletingdateindex = -1;
            List<zametka> listchange = new List<zametka>();
            foreach (var item in list)
            {
                if (item == selected)
                {
                    deletingdateindex = c;
                }
                else
                {
                    listchange.Add(item);
                }
                c++;
            }
            list.Clear();
            list = listchange;
            finalsum -= Convert.ToInt32(selected.Sum);
            dgrid.ItemsSource = null;
            List<zametka> listd = new List<zametka>();
            int a = 0;
            foreach (var item in list)
            {
                if (Dtp.Text == Convert.ToString(item.Date))
                {
                    listd.Add(list[a]);
                }
                a++;
            }
            dgrid.ItemsSource = null;
            dgrid.ItemsSource = listd;
            finalsumw();
        }


        private void finalsumw()
        {
            res.Text = "Итог: " + Convert.ToString(finalsum);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            serialize.Serialize(list);
        }

        private void Dtp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            List<zametka> listd = new List<zametka>();
            int a = 0;
            foreach (var item in list)
            {
                if (Dtp.Text == Convert.ToString(item.Date))
                {
                    listd.Add(list[a]);
                }
                a++;
            }
            dgrid.ItemsSource = null;
            dgrid.ItemsSource = listd;
            finalsumw();
        }

        private void dgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            zametka select_changed = dgrid.SelectedItem as zametka;
            int a = 0;
            foreach (var item in list)
            {
                if (select_changed == item)
                {
                    namebox.Text = item.Name; ;
                    moneybox.Text = item.Sum;
                    cbox.SelectedItem = item.Type;
                    break;
                }
                a++;
            }
        }
    }
}
