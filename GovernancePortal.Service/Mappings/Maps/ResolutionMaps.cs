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
            };
        newVoting.Voters = createVotingPost.Voters.Select(x => InMap(x, newVoting)).ToList();
        return newVoting;
    }

    public VotingUser InMap(VotingUserPOST votingUserPost, Voting voting)
    {
        var votingUser = new VotingUser
        {
            UserId = votingUserPost.UserId,
            VotingId = voting.Id
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

    public Poll InMap(CreatePollingPOST pollPost)
    {
        var newPoll = new Poll
        {
            Title = pollPost.Title,
            isUnlimitedSelection = pollPost.isUnlimitedSelection,
            MaximumSelection = pollPost.MaximumSelection,
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

    public PollUser InMap(PollVotePOST createVotingPost, PollUser pollVoter)
    {
        throw new Exception("The way Poll Votes will be done is not know so this implementation is pending");
    }
}