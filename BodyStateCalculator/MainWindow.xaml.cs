using System;
using System.Windows;
using System.Windows.Controls;

namespace BodyStateCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(HeightTextBox.Text, out int height))
            {
                MessageBox.Show("Некорректно введён рост. Укажите целое число.");
                return;
            }

            if (!double.TryParse(WeightTextBox.Text, out double weight))
            {
                MessageBox.Show("Некорректно введён вес. Укажите число.");
                return;
            }

            if (height < 130 || height > 220)
            {
                MessageBox.Show("Рост должен быть в диапазоне 130-220 см!");
                return;
            }
            if (weight < 40 || weight > 170)
            {
                MessageBox.Show("Вес должен быть в диапазоне 40-170 кг!");
                return;
            }

            string gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (gender == null)
            {
                MessageBox.Show("Выберите пол!");
                return;
            }

            double normalWeight;
            if (gender == "Мужчина")
            {
                normalWeight = (height - 100) * 1.13;
            }
            else
            {
                normalWeight = (height - 100) * 0.90;
            }

            double diff = weight - normalWeight;
            string weightStatus;

            if (diff < -3)
            {
                weightStatus = "ниже нормы";
            }
            else if (diff > 3)
            {
                weightStatus = "выше нормы";
            }
            else
            {
                weightStatus = "норма";
            }

            string result = $"Ваш оптимальный вес: {normalWeight:F1} кг\n" +
                            $"Ваш текущий вес на {Math.Abs(diff):F1} кг " +
                            (diff > 0 ? "больше" : "меньше") +
                            $" нормы. Итог: {weightStatus}.";

            ResultTextBlock.Text = result;
        }
    }
}