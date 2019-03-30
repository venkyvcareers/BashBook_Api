﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BashBook.DAL.EDM
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BashBookEntities : DbContext
    {
        public BashBookEntities()
            : base("name=BashBookEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<EntityPoll> EntityPolls { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventGalary> EventGalaries { get; set; }
        public virtual DbSet<EventGroup> EventGroups { get; set; }
        public virtual DbSet<EventUser> EventUsers { get; set; }
        public virtual DbSet<Ground> Grounds { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupUser> GroupUsers { get; set; }
        public virtual DbSet<LogInfo> LogInfoes { get; set; }
        public virtual DbSet<LookUpValue> LookUpValues { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<MatchQuestion> MatchQuestions { get; set; }
        public virtual DbSet<MatchUserAnswer> MatchUserAnswers { get; set; }
        public virtual DbSet<MatchUserScore> MatchUserScores { get; set; }
        public virtual DbSet<MatchUserTeam> MatchUserTeams { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Poll> Polls { get; set; }
        public virtual DbSet<PollCategory> PollCategories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostComment> PostComments { get; set; }
        public virtual DbSet<PostLike> PostLikes { get; set; }
        public virtual DbSet<PostShare> PostShares { get; set; }
        public virtual DbSet<PostStatInfo> PostStatInfoes { get; set; }
        public virtual DbSet<PredictionCategory> PredictionCategories { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionRule> QuestionRules { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamPlayer> TeamPlayers { get; set; }
        public virtual DbSet<Tournament> Tournaments { get; set; }
        public virtual DbSet<TournamentQuestion> TournamentQuestions { get; set; }
        public virtual DbSet<TournamentTeam> TournamentTeams { get; set; }
        public virtual DbSet<TournamentUserAnswer> TournamentUserAnswers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserContact> UserContacts { get; set; }
        public virtual DbSet<UserOccation> UserOccations { get; set; }
        public virtual DbSet<UserVote> UserVotes { get; set; }
    }
}
