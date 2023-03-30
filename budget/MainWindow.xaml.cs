using Budget.Class;
using Budget.json;
using Budget.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Budget
{
    public partial class MainWindow : Window
    {
        private double money = 0;
        private static List<Note> notes = new List<Note>();
        public static List<string> type = new List<string>();
        private List<Note> NoteFromDataGrid = new List<Note>(); public MainWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            Update_TypeBox();
            DatePick.SelectedDate = DateTime.Now;
            notes = FileManager.ReadFromFile<Note>("Notes.json");
            if (notes != null) { ShowNotes(); }
        }

        private void Update_TypeBox()
        {
            type = FileManager.ReadFromFile<string>("Type.json");
            if (type == null)
            {
                type = new List<string>();
            }
            Notes_type.Items.Clear();
            foreach (string t in type)
            {
                Notes_type.Items.Add(t);
            }
        }

        private void ShowNotes()
        {
            NoteFromDataGrid = new List<Note>();
            DataGridMenu.ItemsSource = null;
            foreach (Note note in notes)
            {
                if (DatePick.Text == note.date)
                {
                    NoteFromDataGrid.Add(note);
                }
            }
            DataGridMenu.ItemsSource = NoteFromDataGrid;
            ShowBalance();
        }

        private void ShowBalance()
        {
            money = notes.Sum(note => note.money);
            Balance_status.Text = "Сумма: " + money.ToString();
        }

        private void Create_new_type(object sender, RoutedEventArgs e)
        {
            CreateTypeWindow createTypeWindow = new CreateTypeWindow();
            createTypeWindow.ShowDialog();
            Update_TypeBox();
        }

        private void Add_new_note_Click(object sender, RoutedEventArgs e)
        {
            double summ;
            if (double.TryParse(summ_money.Text, out summ))
            {
                if (notes == null) { notes = new List<Note>(); }
                DateTime date = Convert.ToDateTime(DatePick.Text);
                Note note = new Note(Name_Note.Text, Notes_type.Text, summ, date.ToShortDateString());
                Name_Note.Text = "";
                summ_money.Text = "";
                notes.Add(note);
                FileManager.MySerialize("Notes.json", notes);
                ShowNotes();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void Delete_Click_1(object sender, RoutedEventArgs e)
        {
            notes.Remove((Note)(DataGridMenu.SelectedItem));
            ShowNotes();
            FileManager.MySerialize("Notes.json", notes);
        }

        private void Change_click(object sender, RoutedEventArgs e)
        {
            if (DataGridMenu.SelectedItem != null)
            {
                int selectedIndex = NoteFromDataGrid.IndexOf((Note)DataGridMenu.SelectedItem);
                int index = notes.IndexOf(NoteFromDataGrid[selectedIndex]);

                if (double.TryParse(summ_money.Text, out double summ))
                {
                    DateTime date = Convert.ToDateTime(DatePick.Text);
                    Note updatedNote = new Note(Name_Note.Text, Notes_type.Text, summ, date.ToShortDateString());
                    notes[index] = updatedNote;
                    Name_Note.Text = "";
                    summ_money.Text = "";
                    FileManager.MySerialize("Notes.json", notes);
                    ShowNotes();
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void DataGrid_DataChange(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridMenu.SelectedItem != null)
            {
                Note selectedNote = (Note)DataGridMenu.SelectedItem;
                Name_Note.Text = selectedNote.nameNote;
                Notes_type.Text = selectedNote.type;
                summ_money.Text = selectedNote.money.ToString();
            }
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "date")
            {
                e.Column = null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowNotes();
        }
    }
}
