﻿using System;
using System.Collections.Generic;

namespace BungieSharper.Schema.Forum
{
    [Flags]
    public enum ForumTopicsCategoryFiltersEnum
    {
        None = 0,

        Links = 1,

        Questions = 2,

        AnsweredQuestions = 4,

        Media = 8,

        TextOnly = 16,

        Announcement = 32,

        BungieOfficial = 64,

        Polls = 128
    }

    public enum ForumTopicsQuickDateEnum
    {
        All = 0,

        LastYear = 1,

        LastMonth = 2,

        LastWeek = 3,

        LastDay = 4
    }

    public enum ForumTopicsSortEnum : byte
    {
        Default = 0,

        LastReplied = 1,

        MostReplied = 2,

        Popularity = 3,

        Controversiality = 4,

        Liked = 5,

        HighestRated = 6,

        MostUpvoted = 7
    }

    public class PostResponse
    {
        public DateTime lastReplyTimestamp { get; set; }

        public bool IsPinned { get; set; }

        public Forum.ForumMediaType urlMediaType { get; set; }

        public string thumbnail { get; set; }

        public Forum.ForumPostPopularity popularity { get; set; }

        public bool isActive { get; set; }

        public bool isAnnouncement { get; set; }

        public int userRating { get; set; }

        public bool userHasRated { get; set; }

        public bool userHasMutedPost { get; set; }

        public long latestReplyPostId { get; set; }

        public long latestReplyAuthorId { get; set; }

        public Ignores.IgnoreResponse ignoreStatus { get; set; }

        public string locale { get; set; }
    }

    public enum ForumMediaType
    {
        None = 0,

        Image = 1,

        Video = 2,

        Youtube = 3
    }

    public enum ForumPostPopularity
    {
        Empty = 0,

        Default = 1,

        Discussed = 2,

        CoolStory = 3,

        HeatingUp = 4,

        Hot = 5
    }

    public class PostSearchResponse : Queries.SearchResult
    {
        public IEnumerable<Forum.PostResponse> relatedPosts { get; set; }

        public IEnumerable<User.GeneralUser> authors { get; set; }

        public IEnumerable<GroupsV2.GroupResponse> groups { get; set; }

        public IEnumerable<Tags.Models.Contracts.TagResponse> searchedTags { get; set; }

        public IEnumerable<Forum.PollResponse> polls { get; set; }

        public IEnumerable<Forum.ForumRecruitmentDetail> recruitmentDetails { get; set; }

        public int? availablePages { get; set; }

        public IEnumerable<Forum.PostResponse> results { get; set; }
    }

    public class PollResponse
    {
        public long topicId { get; set; }

        public IEnumerable<Forum.PollResult> results { get; set; }

        public int totalVotes { get; set; }
    }

    public class PollResult
    {
        public string answerText { get; set; }

        public int answerSlot { get; set; }

        public DateTime lastVoteDate { get; set; }

        public int votes { get; set; }

        public bool requestingUserVoted { get; set; }
    }

    public class ForumRecruitmentDetail
    {
        public long topicId { get; set; }

        public bool microphoneRequired { get; set; }

        public Forum.ForumRecruitmentIntensityLabel intensity { get; set; }

        public Forum.ForumRecruitmentToneLabel tone { get; set; }

        public bool approved { get; set; }

        public long? conversationId { get; set; }

        public int playerSlotsTotal { get; set; }

        public int playerSlotsRemaining { get; set; }

        public IEnumerable<User.GeneralUser> Fireteam { get; set; }

        public IEnumerable<long> kickedPlayerIds { get; set; }
    }

    public enum ForumRecruitmentIntensityLabel : byte
    {
        None = 0,

        Casual = 1,

        Professional = 2
    }

    public enum ForumRecruitmentToneLabel : byte
    {
        None = 0,

        FamilyFriendly = 1,

        Rowdy = 2
    }

    public enum ForumPostSortEnum
    {
        Default = 0,

        OldestFirst = 1
    }

    public enum CommunityContentSortMode : byte
    {
        Trending = 0,

        Latest = 1,

        HighestRated = 2
    }
}