﻿using Practice_6.Helpers;
using Practice_6.Models;
using Practice_6.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Practice_6.ViewModels
{
    public class PublisherVM : NotifyPropertyChanged
    {
        private PublisherRepository publisherRepository;
        public PublisherVM()
        {
            publisherRepository = new PublisherRepository();
            Publishers = publisherRepository.GetPublishersList();
            AddPublisher = new RelayCommand(AddPublisherFunc);
            FindPublisher = new RelayCommand(FindPublisherFunc);
            GetPublishers = new RelayCommand(GetPublishersFunc);
        }

        public ObservableCollection<Publisher> _publishers;
        public ObservableCollection<Publisher> Publishers
        {
            get => _publishers;
            set
            {
                _publishers = value;
                OnPropertyChanged("Publishers");
            }
        }

        private string _publisherNameText;

        public string PublisherNameText
        {
            get { return _publisherNameText; }
            set
            {
                _publisherNameText = value;
                OnPropertyChanged("LastNameText");
            }
        }

        public RelayCommand AddPublisher { get; }
        public RelayCommand FindPublisher { get; }
        public RelayCommand GetPublishers { get; }

        public void AddPublisherFunc(object obj)
        {
            if (PublisherNameText == null) return;
            publisherRepository.AddPublisherToList(PublisherNameText);
            Publishers = publisherRepository.GetPublishersList();
        }

        public void FindPublisherFunc(object obj)
        {
            Publishers = publisherRepository.FindPublishersList(PublisherNameText);
        }

        public void GetPublishersFunc(object obj)
        {
            Publishers = publisherRepository.GetPublishersList();
        }
    }
}
