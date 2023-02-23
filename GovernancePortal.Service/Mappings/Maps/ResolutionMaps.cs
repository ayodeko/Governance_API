using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GovernancePortal.Core.Resolutions;
using GovernancePortal.Service.Interface;
using GovernancePortal.Service.Mappings.IMaps;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace GovernancePortal.Service.Mappings.Maps;

class ResolutionAutoMapper : Profile
{
    public ResolutionAutoMapper()
    {
        CreateMap<Voting, VotingDetailsGET>();
        CreateMap<VotingUser, VotingUserGET>();
        CreateMap<Poll, UpdatePollingPOST>();
    }
}

public class ResolutionMaps : IResolutionMaps
{
    private readonly IMapper _autoMapper;
    private IResolutionMaps _resolutionMapsImplementation;

    public ResolutionMaps()
    {
        var profiles = new List<Profile>() { new ResolutionAutoMapper() };
        var mapperConfiguration = new MapperConfiguration(config => config.AddProfiles(profiles));
        _autoMapper = mapperConfiguration.CreateMapper();
    }
    public Voting InMap(CreateVotingPOST createVotingPost)
    {
        var newVoting = new Voting
        {
            Title = createVotingPost.Title,
            Summary = createVotingPost.Summary,
            IsAnonymous = createVotingPost.IsAnonymous,
            IsPast = createVotingPost.IsPast,
            DateTime = createVotingPost.DateTime,
            };
        newVoting.Voters = createVotingPost.Voters.Select(x => InMap(x, newVoting)).ToList();
        return newVoting;
    }

    public VotingUser InMap(VotingUserPOST votingUserPost, Voting voting)
    {
        var votingUser = new VotingUser
        {
            UserId = votingUserPost.UserId,
            VotingId = voting.Id,
            Stance = votingUserPost.Stance,
            StanceReason = votingUserPost.StanceReason
        };
        return votingUser;
    }

    public VotingUser InMap(VotePOST votePost, VotingUser votingUser)
    {
        votingUser.Stance = votePost.Stance;
        votingUser.StanceReason = votePost.StanceReason;
        return votingUser;
    }

    public VotingDetailsGET OutMap(Voting voting)
    {
        return _autoMapper.Map(voting, new VotingDetailsGET());
    }
    public UpdatePollingPOST OutMap(Poll poll)
    {
        return _autoMapper.Map(poll, new UpdatePollingPOST());
    }

    public Poll InMap(CreatePollingPOST pollPost)
    {
        var newPoll = new Poll
        {
            Title = pollPost.Title,
            isUnlimitedSelection = pollPost.isUnlimitedSelection,
            IsAnonymousVote = pollPost.IsAnonymousVote,
            MaximumSelection = pollPost.MaximumSelection,
            DateTIme = pollPost.DateTime
        };
        newPoll.PollItems = pollPost.PollItems.Select(x => InMap(x, newPoll)).ToList();
        newPoll.PollUsers = pollPost.PollUsers.Select(x => InMap(x, newPoll)).ToList();
        return newPoll;
    }

    private PollItem InMap(PollItemPOST pollItemPost, Poll poll)
    {
        return new PollItem
        {
            Title = pollItemPost.Title
        };
    }
    private PollUser InMap(PollUserPOST pollUserPost, Poll poll)
    {
        return new PollUser
        {
            PollId = poll.Id,
            UserId = pollUserPost.UserId
        };
    }
    
    public List<PollItemVote> InMap(PollVotePOST pollVotePost, List<PollItemVote> preexistingPollItemVotes)
    {
        var votedItemIds = pollVotePost.PollItemIds;
        var returnList = votedItemIds.Select(x => InMap(pollVotePost.UserId, x, preexistingPollItemVotes));
        return returnList.ToList();
    }

    private PollItemVote InMap(string userId, string pollItemId, List<PollItemVote> preexistingPollItemVotes)
    {
        var alreadyVoted = preexistingPollItemVotes.FirstOrDefault(x => x.PollItemId == pollItemId && x.UserId == userId);
        return alreadyVoted ?? new PollItemVote()
        {
            PollItemId = pollItemId,
            UserId = userId
        };
    }

    public Poll InMap(CreatePastPollPOST pollPost)
    {
        var newPoll = new Poll
        {
            Title = pollPost.Title,
            PastPollParticipantAmount = pollPost.PastPollParticipantAmount,
            DateTIme = pollPost.DateTime
        };
        newPoll.PastPollItems = pollPost.PastPollItems.Select(x => InMap(x, newPoll)).ToList();
        return newPoll;
    }

    public Poll InMap(UpdatePollingPOST updatePollPost, Poll preexistingPoll)
    {
        preexistingPoll.Title = updatePollPost.Title;
        preexistingPoll.isUnlimitedSelection = updatePollPost.isUnlimitedSelection;
        preexistingPoll.IsAnonymousVote = updatePollPost.IsAnonymousVote;
        preexistingPoll.MaximumSelection = updatePollPost.MaximumSelection;
        preexistingPoll.DateTIme = updatePollPost.DateTime;
        return preexistingPoll;
    }

    private PastPollItem InMap(PastPollItemPOST createPollPost, Poll poll)
    {
        return new PastPollItem()
        {
            Title = createPollPost.Title,
            Percentage = createPollPost.Percentage
        };
    }
}