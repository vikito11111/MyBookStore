﻿namespace MyBookStore.Models
{
    public class Forum
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<Topic> Topics { get; set; }
    }
}