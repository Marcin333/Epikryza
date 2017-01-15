using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplicationEpikryza
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'forHealthDBDataSet.tblDanePacjenta' table. You can move, or remove it, as needed.
            this.tblDanePacjentaTableAdapter.Fill(this.forHealthDBDataSet.tblDanePacjenta);

        }

        private void btnSzukaj_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(textSearchBox.Text)) && textSearchBox.Text.Length == 11)
            {
                var query = from d in this.forHealthDBDataSet.tblDanePacjenta
                            where d.Pesel == textSearchBox.Text
                            select d;

                var today = DateTime.Today;
                var a = (today.Year * 100 + today.Month) * 100 + today.Day;
                string kobieta = "Kobieta";
                string men = "Mężczyzna";
                if (query.Any())
                {
                    foreach (var n in query)
                    {
                        var b = (n.Data_Urodzenia.Year * 100 + n.Data_Urodzenia.Month) * 100 + n.Data_Urodzenia.Day;
                        int age = (a - b) / 10000;

                        textBoxPacjent_ID.Text = n.ID_Pacjenta.ToString();
                        textDataRejestracji.Text = today.ToString("yyyy/MM/dd");
                        textNazwisko.Text = n.Nazwisko;
                        textImie.Text = n.Imie;
                        textBoxPesel.Text = n.Pesel;
                        textWiek.Text = n.Data_Urodzenia.ToString("yyyy/MM/dd");
                        textAge.Text = age.ToString();
                        textPlec.Text = (n.Plec.Contains("1")) ? kobieta : men;
                        textTel.Text = n.Tel_Komorkowy;
                        textEmail.Text = n.Email;
                        textMiasto.Text = n.Miasto_Zam;
                    }
                    textSearchBox.Text = String.Empty;
                }
                else
                {
                    MessageBox.Show("Nie znaleziono rekordu. \nSprawdź poprawność wpisanego numeru pesel.");
                }
               
            } else
            {
                MessageBox.Show("Nie znaleziono rekordu.");
                textSearchBox.Text = String.Empty;

            }
        }

        private void btnAlergen_Click(object sender, EventArgs e)
        {
            
        }

        private void bDodajAlergen_Click(object sender, EventArgs e)
        {
            Object selectedItem = comboBAlergen.SelectedItem;
            this.dataGridViewAlergen.Rows.Add(selectedItem);
            comboBAlergen.Text = String.Empty;
            int counter;
            int alergenyTotal = 0;

            for (counter = 0; counter < (dataGridViewAlergen.Rows.Count);
               counter++)
            {
                if (dataGridViewAlergen.Rows[counter].Cells["alergen"].Value
                    != null)
                {
                    if (dataGridViewAlergen.Rows[counter].
                        Cells["alergen"].Value.ToString().Length != 0)
                    {
                        alergenyTotal += 1;
                        this.textBoxCountAlergeny.Text = alergenyTotal.ToString();
                        //Console.WriteLine(alergenyTotal);
                    }
                }
            }
        }

        private void btnDodajViofo_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedItemMata = comboBoxMata.Text;
                string mata = selectedItemMata.Length > 0 ? selectedItemMata.ToString().Substring(10) : selectedItemMata;
                Object selectedItemPx = comboBoxPx.SelectedItem;
                Object selectedItemMx = comboBoxMx.SelectedItem;
                string natezenie = comboBoxNatezenie.Text;
                string krotnosc = numericUDKrotnosc.Value.ToString();
                string data = String.Format("{0}x {1}{2}{3}({4}) ", krotnosc, mata, selectedItemPx, 
                            selectedItemMx, natezenie);
                //myComboBox.Text.Clear();
                //this.dataGridViewViofor.Rows.Add();
                if (radioBtnRano.Checked)
                {
                    textBoxWRano.Text = data;
                }
                if (radioBtnPopoludniu.Checked)
                {
                    textBoxWPoPoludniu.Text = data;
                }
                if (radioBtnWieczor.Checked)
                {
                    textBoxWwieczor.Text = data;
                }
                
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        private void panelNiezyt_Paint(object sender, PaintEventArgs e)
        {
            //Rectangle r = new Rectangle(0, 0, 465, 76); //this.ClientRectangle.Height - 1
            //Pen p = new Pen(Color.White, 2);
            //e.Graphics.DrawRectangle(p, r);
        }

        private void radioBtnPrzewlekly_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Yellow;
            if (radioBtnPodostry.Checked || radioBtnOstry.Checked)
            {
                ctrl.BackColor = SystemColors.GradientInactiveCaption;
            }
        }

        private void radioBtnNiezyt_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Orange;
            if (radioBtnPrzewlekly.Checked || radioBtnOstry.Checked)
            {
                ctrl.BackColor = SystemColors.GradientInactiveCaption;
            }
        }

        private void radioBtnOstry_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Red;
            if (radioBtnPodostry.Checked || radioBtnPrzewlekly.Checked)
            {
                ctrl.BackColor = SystemColors.GradientInactiveCaption;
            }
        }

        private void radioBtnRano_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.MenuHighlight;
        }

        private void radioBtnPopoludniu_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.MenuHighlight;
        }

        private void radioBtnWieczor_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.MenuHighlight;
        }

        private void btnZapiszWszystko_Click(object sender, EventArgs e)
        {
            try
            {
                // Vioforoterapia
                if (!(String.IsNullOrEmpty(textBoxWRano.Text)))
                {
                    string rano = textBoxWRano.Text;
                }
                if (!(String.IsNullOrEmpty(textBoxWPoPoludniu.Text)))
                {
                    string poPoludniu = textBoxWPoPoludniu.Text;
                }
                if (!(String.IsNullOrEmpty(textBoxWwieczor.Text)))
                {
                    string wieczor = textBoxWwieczor.Text;
                }

                // Wywiad
                string podstawowaKontrolna = radioBtnPodstawowa.Checked ?
                            radioBtnPodstawowa.Text : radioBtnKontrolna.Text;
                Console.WriteLine("podstawowaKontrolna - {0}", podstawowaKontrolna);
                
                if (radioBtnOmdleniaTak.Checked || radioBtnOmdleniaNie.Checked)
                {
                    var omdlenia = groupBoxOmdlenia.Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
                    Console.WriteLine("omdlenia - {0}", omdlenia.Text);
                }

                if (radioBtnNieprzytTak.Checked || radioBtnNieprzytTak.Checked)
                {
                    var nieprzytomny = groupBoxNieprzytomny.Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
                    Console.WriteLine("nieprzytomny - {0}", nieprzytomny.Text);
                }

                if (radioBtnPrzewlekly.Checked || radioBtnPodostry.Checked || radioBtnOstry.Checked)
                {
                    var niezytRodzaj = groupBoxRodzaje.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
                    Console.WriteLine("niezyt rodzaj - {0}", niezytRodzaj.Text);
                } 
               
                if (!(String.IsNullOrEmpty(comboBoxGrypy.Text)))
                {
                    string grypy = comboBoxGrypy.Text;
                    Console.WriteLine("Grypy {0}", grypy);
                }

                if (!(String.IsNullOrEmpty(comboBoxNiezyty.Text)))
                {
                    Object niezyty = comboBoxNiezyty.Text;
                    Console.WriteLine("niezyty {0}", niezyty);
                }

                if (!(String.IsNullOrEmpty(comboBoxAnginy.Text)))
                {
                    string anginy = comboBoxAnginy.Text;
                    Console.WriteLine("anginy {0}", anginy);
                }

                if (!(String.IsNullOrEmpty(comboBoxWypadki.Text)))
                {
                    string wypadki = comboBoxWypadki.Text;
                    Console.WriteLine("wypadki {0}", wypadki);
                }

                foreach (DataGridViewRow row in dataGridViewSzpital.Rows)
                {
                    if (row.Cells["szpital"].Value != null)
                    {
                        if (row.Cells["szpital"].Value.ToString().Length != 0)
                        {
                            Console.Write("szpital - {0}", row.Cells["szpital"].Value);
                        }
                    }
                }

                foreach (DataGridViewRow row in dataGridViewBole.Rows)
                {
                    if (row.Cells["bole"].Value != null)
                    {
                        if (row.Cells["bole"].Value.ToString().Length != 0)
                        {
                            Console.WriteLine("Bole - {0}", row.Cells["bole"].Value);
                            Console.WriteLine("Sporadyczne - {0}", row.Cells["colSporadyczne"].Value);
                            Console.WriteLine("Przewlekle - {0}", row.Cells["colPrzewlekle"].Value);
                            Console.WriteLine("Ostre - {0}", row.Cells["colOstre"].Value);            
                        }
                    }
                }

                foreach (DataGridViewRow row in dataGridViewDolegliwosci.Rows)
                {
                    if (row.Cells["inneDolegliwosci"].Value != null)
                    {
                        if (row.Cells["inneDolegliwosci"].Value.ToString().Length != 0)
                        {
                            Console.Write("inneDolegliwosci - {0}", row.Cells["inneDolegliwosci"].Value);
                        }
                    }
                }

                foreach (DataGridViewRow row in dataGridViewLeki.Rows)
                {
                    if (row.Cells["lekiWywiad"].Value != null)
                    {
                        if (row.Cells["lekiWywiad"].Value.ToString().Length != 0)
                        {
                            Console.WriteLine("lekiWywiad - {0}", row.Cells["lekiWywiad"].Value);
                        }
                    }
                }

                foreach (DataGridViewRow row in dataGridViewSuplementy.Rows)
                {
                    if (row.Cells["suplementyWywiad"].Value != null)
                    {
                        if (row.Cells["suplementyWywiad"].Value.ToString().Length != 0)
                        {
                            Console.WriteLine("suplementyWywiad - {0}", row.Cells["suplementyWywiad"].Value);
                        }
                    }
                }

                if (!(String.IsNullOrEmpty(comboBoxFaza1.Text)) && !(String.IsNullOrEmpty(comboBoxFaza2.Text)) 
                        && !(String.IsNullOrEmpty(comboBoxFaza3.Text)) && !(String.IsNullOrEmpty(comboBoxFaza4.Text)))
                {
                    // fazy
                }

                foreach (DataGridViewRow row in dataGridViewZywienie.Rows)
                {
                    if (row.Cells["zywienie"].Value != null)
                    {
                        if (row.Cells["zywienie"].Value.ToString().Length != 0)
                        {
                            Console.WriteLine("zywienie - {0}", row.Cells["zywienie"].Value);
                        }
                    }
                }

                foreach (DataGridViewRow row in dataGridViewSupl.Rows)
                {
                    if (row.Cells["Producent"].Value != null && row.Cells["Produkt"].Value != null)
                    {
                        if (row.Cells["Producent"].Value.ToString().Length != 0  && 
                            row.Cells["Produkt"].Value.ToString().Length != 0)
                        {
                            Console.WriteLine("Producent - {0}", row.Cells["Producent"].Value);
                            Console.WriteLine("Produkt - {0}", row.Cells["Produkt"].Value);
                            Console.WriteLine("Ilosc_opakowan - {0}", row.Cells["Ilosc_opakowan"].Value);
                            Console.WriteLine("Na_czczo - {0}", row.Cells["Na_czczo"].Value);
                            Console.WriteLine("Do_sniadania - {0}", row.Cells["Do_sniadania"].Value);
                            Console.WriteLine("Do_kolacji - {0}", row.Cells["Do_kolacji"].Value);
                            Console.WriteLine("Przed_snem - {0}", row.Cells["Przed_snem"].Value);
                        }
                    }
                }

                foreach (DataGridViewRow row in dataGridViewAlergen.Rows)
                {
                    if (row.Cells["alergen"].Value != null)
                    {
                        if (row.Cells["alergen"].Value.ToString().Length != 0)
                        {
                            Console.WriteLine("alergen - {0}", row.Cells["alergen"].Value);
                        }
                    }
                }

                foreach (DataGridViewRow row in dataGridViewOpisAlergii.Rows)
                {
                    if (row.Cells["opisAlergi"].Value != null)
                    {
                        if (row.Cells["opisAlergi"].Value.ToString().Length != 0)
                        {
                            Console.WriteLine("opisAlergi - {0}", row.Cells["opisAlergi"].Value);
                        }
                    }
                }

                foreach (DataGridViewRow row in dataGridViewKHome.Rows)
                {
                    if (row.Cells["kompleksHome"].Value != null && row.Cells["iloscHome"].Value != null)
                    {
                        if (row.Cells["kompleksHome"].Value.ToString().Length != 0 
                            && row.Cells["iloscHome"].Value.ToString().Length != 0)
                        {
                            Console.WriteLine("kompleksHome - {0}", row.Cells["kompleksHome"].Value);
                            Console.WriteLine("iloscHome - {0}", row.Cells["iloscHome"].Value);
                        }
                    }
                }

                foreach (DataGridViewRow row in dataGridViewPato.Rows)
                {
                    if (row.Cells["Patomorfologia"].Value != null)
                    {
                        if (row.Cells["Patomorfologia"].Value.ToString().Length != 0)
                        {
                            Console.WriteLine("Patomorfologia - {0}", row.Cells["Patomorfologia"].Value);
                        }
                    }
                }
                //string omdlenia = radioBtnOmdleniaTak.Checked ? radioBtnOmdleniaTak.Text : radioBtnOmdleniaNie.Text;
                //string niePrzytomny = radioBtnNieprzytTak.Checked ? radioBtnNieprzytTak.Text : radioBtnNieprzytNie.Text;

                //string nieprzytomnyTak = radioBtnNieprzytTak.Checked.ToString();
                //bool podstawowa = radioBtnPodstawowa.Checked; //? radioBtnPodstawowa.Text : null;
                //string kontrolna = radioBtnKontrolna.Checked ? radioBtnKontrolna.Text : null;
                //Console.WriteLine("++++ {0} {1}", podstawowa, kontrolna);
                //Note that this requires that all of the radio buttons be directly in the same container(eg, Panel or Form), and that there is only one group in the container. If that is not the case, you could make List< RadioButton > s in your constructor for each group, then write list.FirstOrDefault(r => r.Checked).


                //Console.Write("+++ Niezyt rodzaj {0} omdlenia {1} nieprzytomny {2} wizyta Pod Kon {3}", niezytRodzaj.Text, omdlenia.Text, nieprzytomny.Text, wizytaPodKon.Text);
                //GetRadioBtnText(radioBtnOmdleniaTak);
                //object omdlenia = radiobtnom
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetRadioBtnText(RadioButton radioBtn)
        {
            if (radioBtn.Checked)
            {
                string radioBtnText = radioBtn.Text;
            }
        }

        private void DodajSzpital_Click(object sender, EventArgs e)
        {   
            if (!(String.IsNullOrEmpty(textBoxSzpital.Text)))
            {
                this.dataGridViewSzpital.Rows.Add(textBoxSzpital.Text);
                textBoxSzpital.Text = String.Empty;
                
            }
            
        }

        private void btnDodajBol_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(comboBoxBole.Text)))
            {
                dataGridViewBole.Rows.Add(comboBoxBole.Text, 
                    checkBoxSporadycznie.Checked ? checkBoxSporadycznie.Text : null,
                    checkBoxPrzewlekle.Checked ? checkBoxPrzewlekle.Text : null,
                    checkBoxOstre.Checked ? checkBoxOstre.Text : null);
            }
            comboBoxBole.Text = String.Empty;
            checkBoxSporadycznie.Checked = false;
            checkBoxSporadycznie.BackColor = SystemColors.GradientInactiveCaption;
            checkBoxPrzewlekle.Checked = false;
            checkBoxPrzewlekle.BackColor = SystemColors.GradientInactiveCaption;
            checkBoxOstre.Checked = false;
            checkBoxOstre.BackColor = SystemColors.GradientInactiveCaption;
        }

        private void checkBoxSporadycznie_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Yellow;
        }

        private void checkBoxPrzewlekle_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Orange;
        }

        private void checkBoxOstre_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Red;
        }

        private void btnDodajDoleglo_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(textBoxDolegliwosci.Text)))
            {
                dataGridViewDolegliwosci.Rows.Add(textBoxDolegliwosci.Text);
            }
            textBoxDolegliwosci.Text = String.Empty;
        }

        private void btnDodajLeki_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(textBoxLeki.Text)))
            {
                dataGridViewLeki.Rows.Add(textBoxLeki.Text);
            }
            textBoxLeki.Text = String.Empty;
        }

        private void btnDodajSuplementy_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(textBoxSuplementy.Text)))
            {
                dataGridViewSuplementy.Rows.Add(textBoxSuplementy.Text);
            }
            textBoxSuplementy.Text = String.Empty;
        }

        private void btnDodajZywienie_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(comboBoxZywienie.Text)))
            {
                dataGridViewZywienie.Rows.Add(comboBoxZywienie.Text);
            }
            int counter;
            int zywienieTotal = 0;

            for (counter = 0; counter < (dataGridViewZywienie.Rows.Count);
               counter++)
            {
                if (dataGridViewZywienie.Rows[counter].Cells["zywienie"].Value
                    != null)
                {
                    if (dataGridViewZywienie.Rows[counter].
                        Cells["zywienie"].Value.ToString().Length != 0)
                    {
                        zywienieTotal += 1;
                        this.textBoxCountZywieniowe.Text = zywienieTotal.ToString();
                        //Console.WriteLine(zywienieTotal);
                    }
                }
            }
        }

        private void btnDodajPato_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(comboBoxPatomorfologia.Text)))
            {
                dataGridViewPato.Rows.Add(comboBoxPatomorfologia.Text);
                comboBoxPatomorfologia.Text = String.Empty;
            }
            int counter;
            int patoTotal = 0;

            for (counter = 0; counter < (dataGridViewPato.Rows.Count); counter++)
            {
                if (dataGridViewPato.Rows[counter].Cells["Patomorfologia"].Value != null)
                {
                    if (dataGridViewPato.Rows[counter].Cells["Patomorfologia"].
                        Value.ToString().Length != 0)
                    {
                        patoTotal += 1;
                        this.textCountRowPato.Text = patoTotal.ToString();
                    }
                }
            }

        }

        private void btnDodajHomo_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(comboBoxHomeop.Text)))
            {
                dataGridViewKHome.Rows.Add(comboBoxHomeop.Text, numericUDhome.Value);
                comboBoxHomeop.Text = String.Empty;
            }
            int counter;
            int kompleksTotal = 0;

            for (counter = 0; counter < (dataGridViewKHome.Rows.Count); counter++)
            {
                if (dataGridViewKHome.Rows[counter].Cells["kompleksHome"].Value != null)
                {
                    if (dataGridViewKHome.Rows[counter].Cells["kompleksHome"].
                        Value.ToString().Length != 0)
                    {
                        kompleksTotal += 1;
                        this.textBoxKHomeCount.Text = kompleksTotal.ToString();
                    }
                }
            }
        }

        private void btnDodajOpisAlergii_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(comboBoxOpisAlergii.Text)))
            {
                dataGridViewOpisAlergii.Rows.Add(comboBoxOpisAlergii.Text, numericUDhome.Value);
                comboBoxOpisAlergii.Text = String.Empty;
            }
            int counter;
            int alergenyOpisTotal = 0;

            for (counter = 0; counter < (dataGridViewOpisAlergii.Rows.Count); counter++)
            {
                if (dataGridViewOpisAlergii.Rows[counter].Cells["opisAlergi"].Value != null)
                {
                    if (dataGridViewOpisAlergii.Rows[counter].Cells["opisAlergi"].
                        Value.ToString().Length != 0)
                    {
                        alergenyOpisTotal += 1;
                        this.textCountRowOpisAlergii.Text = alergenyOpisTotal.ToString();
                    }
                }
            }
        }

        private void btnDodajSupl_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(comboBoxProducenci.Text)) && !(String.IsNullOrEmpty(comboBoxProdukty.Text)))
            {
                dataGridViewSupl.Rows.Add(comboBoxProducenci.Text, comboBoxProdukty.Text,
                    numericUdOpakowania.Value, numericUdCzczo.Value, numericUdSniadanie.Value,
                    numericUdKolacji.Value, numericUdKolacji.Value, numericUdSnem.Value);
                comboBoxProducenci.ResetText();
                comboBoxProdukty.Text = String.Empty;
                numericUdOpakowania.Value = 1;
                numericUdCzczo.Value = 1;
                numericUdSniadanie.Value = 1;
                numericUdKolacji.Value = 1;
                numericUdKolacji.Value = 1;
                numericUdSnem.Value = 1;

            }
        }

        private void RowsAddedPatom(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int countPato;
            if ((textCountRowPato.Text != String.Empty))
            {
                countPato = int.Parse(textCountRowPato.Text);
                countPato++;
                textCountRowPato.Text = countPato.ToString();
            }
        }

        private void userDeletedRowPato(object sender, DataGridViewRowEventArgs e)
        {
            int countPato;
            if ((textCountRowPato.Text != String.Empty) && (int.Parse(textCountRowPato.Text) > 0))
            {
                countPato = int.Parse(textCountRowPato.Text);
                countPato--;
                textCountRowPato.Text = countPato.ToString();
            }
        }

        private void rowsAddedHomeo(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int countHomeo;
            if ((textBoxKHomeCount.Text != String.Empty))
            {
                countHomeo = int.Parse(textBoxKHomeCount.Text);
                countHomeo++;
                textBoxKHomeCount.Text = countHomeo.ToString();
            }
        }

        private void userDeletedRowHomeo(object sender, DataGridViewRowEventArgs e)
        {
            int countHomeo;
            if ((textBoxKHomeCount.Text != String.Empty) && (int.Parse(textBoxKHomeCount.Text) > 0))
            {
                countHomeo = int.Parse(textBoxKHomeCount.Text);
                countHomeo--;
                textBoxKHomeCount.Text = countHomeo.ToString();
            }
        }

        private void rowsAddedAlergeny(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int countAlergen;
            if (textBoxCountAlergeny.Text != String.Empty)
            {
                countAlergen = int.Parse(textBoxCountAlergeny.Text);
                countAlergen++;
                textBoxCountAlergeny.Text = countAlergen.ToString();
            }
        }

        private void userDeletedRowAlergen(object sender, DataGridViewRowEventArgs e)
        {
            int countAlergen;
            if ((textBoxCountAlergeny.Text != String.Empty) && (int.Parse(textBoxCountAlergeny.Text) > 0))
            {
                countAlergen = int.Parse(textBoxCountAlergeny.Text);
                countAlergen--;
                textBoxCountAlergeny.Text = countAlergen.ToString();
            }
        }

        private void rowsAddedOpisAlergii(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int countOpisAlergen;
            if (textCountRowOpisAlergii.Text != String.Empty)
            {
                countOpisAlergen = int.Parse(textCountRowOpisAlergii.Text);
                countOpisAlergen++;
                textCountRowOpisAlergii.Text = countOpisAlergen.ToString();
            }
        }

        private void userDeletedRowOpisAlergii(object sender, DataGridViewRowEventArgs e)
        {
            int countOpisAlergen;
            if ((textCountRowOpisAlergii.Text != String.Empty) && (int.Parse(textCountRowOpisAlergii.Text) > 0))
            {
                countOpisAlergen = int.Parse(textCountRowOpisAlergii.Text);
                countOpisAlergen--;
                textCountRowOpisAlergii.Text = countOpisAlergen.ToString();
            }
        }

        private void btnDodajZabiegChirurgiczny_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(txtZabiegiChirurgiczne.Text)))
            {
                dgvZabiegiChirurgiczne.Rows.Add(txtZabiegiChirurgiczne.Text);
                txtZabiegiChirurgiczne.Text = String.Empty;
            }
                
        }

        private void btnDodajAtaki_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(txtAtaki.Text)))
            {
                dgvAtaki.Rows.Add(txtAtaki.Text);
                txtAtaki.Text = String.Empty;
            }
        }

        private void btnMen_Click(object sender, EventArgs e)
        {
            imgBoxPenis.Visible = true;
        }
    }
}

/*private void userDeletedAlergen(object sender, DataGridViewRowEventArgs e)
        {
            // !uwaga if textCountRowAlergeny.Text == "" is not gonna count
            // uwaga 2 sprawdzanie czy wartosc zostala wybrana z combobox
            int countAlergeny;
            if (textCountRowAlergeny.Text != "")
            {
                countAlergeny = int.Parse(textCountRowAlergeny.Text);
                textCountRowAlergeny.Text = countAlergeny.ToString();
                if (countAlergeny >= 0)     //&& !(countAlergeny < 0)
                {
                    countAlergeny--;
                    textCountRowAlergeny.Text = countAlergeny.ToString();
                } 
            }
        }

        private void userAddedAlergen(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int countAlergeny;
            if (textCountRowAlergeny.Text != "")
            {
                countAlergeny = int.Parse(textCountRowAlergeny.Text);
                countAlergeny++;
                textCountRowAlergeny.Text = countAlergeny.ToString();  
            }
            
            
        }
  
 */
