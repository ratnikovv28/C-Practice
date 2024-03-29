﻿using Practice_6.Helpers;
using Practice_6.Repositories;
using Practice_6.ViewModels;

namespace Practice_6.Models
{
    public class Publisher
    {
        private readonly PublisherRepository publisherRepository;
        private PublisherVM publisherVM;
        public int PublisherId { get; }
        public string PublisherName { get; }

        public Publisher(int publisherId, string publisherName)
        {
            PublisherId = publisherId;
            PublisherName = publisherName;
            DeletePublisher = new RelayCommand(DeletePublisherFunc);
            publisherRepository = new PublisherRepository();
        }

        #region Command's
        public RelayCommand DeletePublisher { get; }

        #endregion

        #region Function's
        public void DeletePublisherFunc(object obj)
        {
            publisherVM = AllUC._PublisherVM;
            publisherVM.Publishers.Remove(this);
            publisherRepository.DeletePublisherFromList(PublisherId);
        }
        #endregion
    }
}
