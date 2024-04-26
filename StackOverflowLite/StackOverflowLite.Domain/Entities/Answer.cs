﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowLite.Domain.Entities
{
    public class Answer
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime AnswerPosted { get; set; }
        public int Upvote { get; set; }
        public int Downvote { get; set; }
        public int VoteCount { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public Question Question { get; set; }
        public IList<AnswerComment> AnswerComments { get; set; }
    }
}
