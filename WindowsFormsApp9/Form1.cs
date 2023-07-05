using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        private Dictionary<DateTime, string> notes; // Dictionary to store notes for each date

        public Form1()
        {
            InitializeComponent();
            notes = new Dictionary<DateTime, string>();
            monthCalendar.DateChanged += MonthCalendar_DateChanged; // Attach DateChanged event handler
        }

        private void MonthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDate = monthCalendar.SelectionStart.Date;
            if (notes.ContainsKey(selectedDate))
            {
                string note = notes[selectedDate];
                textBox.Text = note;
            }
            else
            {
                textBox.Text = string.Empty;
            }

            // Update the bolded dates whenever the selected month changes
            BoldDatesWithNotes();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = monthCalendar.SelectionStart.Date;
            string note = textBox.Text.Trim();
            if (!string.IsNullOrEmpty(note))
            {
                if (notes.ContainsKey(selectedDate))
                {
                    notes[selectedDate] = note;
                }
                else
                {
                    notes.Add(selectedDate, note);
                }
            }
            textBox.Clear();

            // Update the bolded dates after adding a new note
            BoldDatesWithNotes();
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = monthCalendar.SelectionStart.Date;
            if (notes.ContainsKey(selectedDate))
            {
                string note = notes[selectedDate];
                MessageBox.Show(note, "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BoldDatesWithNotes()
        {
            // Clear the bolded dates
            monthCalendar.RemoveAllBoldedDates();

            // Bold dates with notes
            foreach (DateTime date in notes.Keys)
            {
                monthCalendar.AddBoldedDate(date);
            }

            monthCalendar.UpdateBoldedDates();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            DateTime startDate = DateTime.Parse(textBoxStartDate.Text);
            DateTime endDate = DateTime.Parse(textBoxEndDate.Text);

            var eventsInRange = notes.Where(pair => pair.Key >= startDate && pair.Key <= endDate);

            StringBuilder sb = new StringBuilder();
            foreach (var pair in eventsInRange)
            {
                sb.AppendLine($"{pair.Key.ToShortDateString()}: {pair.Value}");
            }

            MessageBox.Show(sb.ToString(), "Events within Range", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}