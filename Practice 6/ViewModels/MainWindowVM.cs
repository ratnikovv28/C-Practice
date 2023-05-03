using Microsoft.Win32;
using Practice_6.Helpers;
using Practice_6.Models;
using Practice_6.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;
using Newtonsoft.Json;

namespace Practice_6.ViewModels
{
    public class MainWindowVM : NotifyPropertyChanged
    {
        public readonly DataVM dataVM;
        public MainWindowVM()
        {
            CurrentViewModel = AllUC._DataVM;
            dataVM = AllUC._DataVM;
            LoadData = new RelayCommand(LoadDataFunc);
            SaveData = new RelayCommand(SaveDataFunc);
            OpenAuthors = new RelayCommand(OpenAuthorsFunc);
            OpenPublishers = new RelayCommand(OpenPublishersFunc);
            OpenData = new RelayCommand(OpenDataFunc);
        }

        #region Property's
        private object _currentviewmodel;
        public object CurrentViewModel
        {
            get => _currentviewmodel;
            set
            {
                _currentviewmodel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }
        #endregion

        #region Command's
        public RelayCommand LoadData { get; }

        public RelayCommand SaveData { get; }

        public RelayCommand OpenData { get; }
        public RelayCommand OpenAuthors { get; }
        public RelayCommand OpenPublishers { get; }
        #endregion

        #region Function's
        public void LoadDataFunc(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON файл (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Читаем содержимое файла
                    string json = File.ReadAllText(openFileDialog.FileName);

                    // Десериализуем JSON в список объектов Book
                    List<Book> books = JsonConvert.DeserializeObject<List<Book>>(json);

                    //dataVM.Books.Clear();
                    dataVM.bookRepository.DeleteAllFromList();
                    // Добавляем книги в коллекцию в ViewModel
                    foreach (Book book in books)
                    {
                        //dataVM.Books.Add(book);
                        dataVM.bookRepository.AddBookToListByNames(book.BookName, book.AuthorName, book.PublisherName);
                    }
                    dataVM.Books = dataVM.bookRepository.GetBooksList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                   
            }
        }

        public void SaveDataFunc(object obj)
        {
            // Открываем диалоговое окно для выбора файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON файл (*.json)|*.json";
            if (saveFileDialog.ShowDialog() == true)
            {
                // Сериализуем список книг в формат JSON
                string json = JsonConvert.SerializeObject(dataVM.Books, Formatting.Indented);

                // Записываем JSON-строку в файл
                File.WriteAllText(saveFileDialog.FileName, json);
            }
        }

        public void OpenDataFunc(object obj)
        {
            CurrentViewModel = AllUC._DataVM;
        }

        public void OpenAuthorsFunc(object obj)
        {
            CurrentViewModel = AllUC._AuthorVM;
        }

        public void OpenPublishersFunc(object obj)
        {
            CurrentViewModel = AllUC._PublisherVM;
        }
        #endregion
    }
}
