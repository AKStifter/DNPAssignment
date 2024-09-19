using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class DummyData
{
    private readonly IUserRepository userRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;

    public DummyData(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public void createDummyData()
    {
        User user1 = new User("TheFirst", "12345");
        User user2 = new User("TheRock", "RockAndRoll");
        User user3 = new User("T", "12345");
        User user4 = new User("OutOfIdeas", "23242");
        User user5 = new User("RadiantRocket", "12345");
        
        userRepository.AddAsync(user1);
        userRepository.AddAsync(user2);
        userRepository.AddAsync(user3);
        userRepository.AddAsync(user4);
        userRepository.AddAsync(user5);

        string title =
            "My Paladin broke his oath and now the entire party is calling me an unfair DM";
        string body =
            "One of my players is a min-maxed blue dragonborn sorcadin build (Oath of Glory/ Draconic Sorcerer) Since he is only " +
            "playing this sort of a character for the damage potential and combat effectiveness, he does not care much about the " +
            "roleplay implications of playing such a combination of classes.\n\nAnyway, in one particular session my players were" +
            " trying to break an NPC out of prison. to plan ahead and gather information, they managed to capture one of the Town " +
            "Guard generals and then interrogate him. The town the players are in is governed by a tyrannical baron who does not " +
            "take kindly to failure. So, fearing the consequences of revealing classified information to the players, the general " +
            "refused to speak. The paladin had the highest charisma and a +6 to intimidation so he decided to lead the " +
            "interrogation, and did some pretty messed up stuff to get the captain to talk, including but not limited to- torture, " +
            "electrocution and manipulation.\n\nI ruled that for an Oath of Glory Paladin he had done some pretty inglorious " +
            "actions, and let him know after the interrogation that he felt his morality break and his powers slowly fade. Both " +
            "the player and the rest of the party were pretty upset by this. The player asked me why I did not warn him beforehand " +
            "that his actions would cause his oath to break, while the rest of the party decided to argue about why his actions " +
            "were justified and should not break the oath of Glory (referencing to the tenets mentioned in the subclass).\n\nI " +
            "decided not to take back my decisions to remind players that their decisions have story repercussions and they can't " +
            "just get away scott-free from everything because they're the \"heroes\". All my players have been pretty upset by this" +
            " and have called me an \"unfair DM\" on multiple occasions. Our next session is this Saturday and I'm considering" +
            " going back on my decision and giving the paladin back his oath and his powers. it would be great to know other " +
            "people's thoughts on the matter and what I should do.\n\nEDIT: for those asking, I did not completely depower my " +
            "Paladin just for his actions. I have informed him that what he has done is considered against his oath, and he does" +
            " get time to atone for his decision and reclaim the oath before he loses his paladin powers.\n\nEDIT 2: thank you " +
            "all for your thoughts on the matter. I've decided not to go back on my rulings and talked to the player, explaining " +
            "the options he has to atone and get his oath back, or alternatively how he can become an Oathbreaker. the player " +
            "decided he would prefer just undergoing the journey and reclaiming his oath by atoning for his mistakes. He talked " +
            "to the rest of the party and they seemed to have chilled out as well.";


        Post post1 = new Post(title, body, 1);
        postRepository.AddAsync(post1);

        title =
            "AITAH? I stopped wearing/using what my husband gave me after he said that it's his money";
        body =
            "I (26f) had been with my husband (30m) for five years, married three months ago. I'm a housewife andI have a little" +
            " side job so I can buy what I want, my husband has a high paying job that covers the all the utilities and bills. " +
            "Just a little background, after we got married, my husband insisted for me to stop working altogether since his " +
            "paycheck can cover everything and help us live comfortably so I agreed.\n\nLast Monday when I got home after I " +
            "bought groceries. He asked how much was it, I told him it's $950 since he has requests and additions to the list. " +
            "If not it will be only $850 just like every month.\n\nAfter that, he got angry at me and told me to stop using his" +
            " paycheck since it's not my money. I explained to him that I followed the list and got his request. He didn't " +
            "listen and said that I'm basically throwing it all away. I was taken aback since I only use his money to pay the " +
            "bills and utilities. I have a side job for my interests and I never ask him something unless I needed it.\n\nI was " +
            "so angry at his accusation that after that day I began to dig up my old stuff and used it instead and I also " +
            "stopped wearing or using his gifts. He confronted me and asked why, I only said that I don't feel like throwing " +
            "his money away, he looked sad and left.\n\nWhen I told my friends about it, they said that what I did was petty " +
            "and I should just listen, some of them said that I should be pettier. My parents are reprimanded me for taking " +
            "things too far. It's been four days now and we haven't talked. I'm starting to think that I really did went too " +
            "far.\n\nAm I the asshole for rejecting his gifts?";
        
        Post post2 = new Post(title, body, 1);
        postRepository.AddAsync(post2);

        title = "Speedrunners: you are literally the worst and I hate you.";
        body =
            "I literally do not understand. Cosmetics are cool I get it.\n\nIt took me six attempts to load into a mission " +
            "because of the joining bug. Finally loaded in, ruthless decapitation mission.\n\nMyself and a level 25 heavy, " +
            "and a bot. Didn’t get a third sadly.\n\nMyself and the bot are fighting and the other player literally rolled " +
            "through the entire mission up to the first assemble point without firing a shot and stood there. Massive enemy " +
            "wave happens and I’m stuck with a bot sniper using only a bolt pistol while they stand there. I went down and " +
            "they quit the mission. This is the fourth time I’ve had someone do this, why?? Just go play pvp to farm your " +
            "cosmetics.";
        
        Post post3 = new Post(title, body, 3);
        postRepository.AddAsync(post3);

        Comment comment1 = new Comment(2, 1, "That guy is the worst");
        Comment comment2 = new Comment(5, 1, "Womp Womp");
        Comment comment3 = new Comment(3, 1, "“I shouldnt have to warn you that Torturing someone is literally evil.” Case closed. " +
                                             "If you are not an Evil alligned creature you cannot torture people and NO torture is NOT NEUTRAL." +
                                             " You cannot neutrally torture someone.");
        commentRepository.AddAsync(comment1);
        commentRepository.AddAsync(comment2);
        commentRepository.AddAsync(comment3);

    }
}