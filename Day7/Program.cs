// Pretty much every AoC project starts with this code, so let's create a template for it.
using System.Diagnostics;

class Program
{
    class CamelCard
    {
        public enum HandType
        {
            HighCard,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            FullHouse,
            FourOfAKind,
            FiveOfAKind
        };

        public string Hand { get; set; }
        public long Bid { get; set; }
        public HandType Type { get; set; }

        // part 1:
        //const char Joker= '!';
        const char Joker= 'J';
        public CamelCard(string hand, string bid)
        {
            this.Hand = hand;
            this.Bid = long.Parse(bid);
            if ( Hand.Contains(Joker))
            { // contains joker requires more work
                this.Type = HandType.HighCard;
                JokerHandType([.. Hand]);
            }
            else
                this.Type = GetHandType(Hand);
            Debug.WriteLine($"hand {hand} is type {Type}");
        }

        private HandType JokerHandType( char[] hand )
        {
            string strHand = new(hand);
            //Console.WriteLine($"Checking joker hand {strHand}");
            int indexFirstJoker = Array.IndexOf( hand, Joker );
            if ( indexFirstJoker == -1 )
            { 
                var t = GetHandType(strHand);
                Debug.WriteLine($"hand {strHand} is test type {t}");
                //Console.WriteLine($"New type {t}");
                return t;
            }

            Debug.WriteLine($"Checking joker hand {strHand}");
            char[] newHand = strHand.ToCharArray();
            foreach (char c in "123456789TQKA")
            {
                newHand[indexFirstJoker] = c;
                HandType chkType = JokerHandType(newHand);
                if ( chkType > Type )
                {
                    Debug.WriteLine($"New type for {strHand} is {chkType}");
                    Type = chkType;
                    if (chkType == HandType.FiveOfAKind)
                        return Type;
                }
            }

            return HandType.HighCard;    
        }
        bool IsFiveOfAKind(string hand)
        {
            int numberOfOccurrences = 5;

            // Check if the string contains at least the specified number of occurrences of any character
            return hand.GroupBy(c => c).Any(group => group.Count() == numberOfOccurrences);
        }

        bool IsFourOfAKind(string hand)
        {
            int numberOfOccurrences = 4;

            // Check if the string contains at least the specified number of occurrences of any character
            return hand.GroupBy(c => c).Any(group => group.Count() == numberOfOccurrences);
        }

        bool IsThreeOfAKind(string hand)
        {
            int numberOfOccurrences = 3;

            // Check if the string contains at least the specified number of occurrences of any character
            return hand.GroupBy(c => c).Any(group => group.Count() == numberOfOccurrences);
        }

        bool IsTwoOfAKind(string hand)
        {
            int numberOfOccurrences = 2;

            // Check if the string contains at least the specified number of occurrences of any character
            return hand.GroupBy(c => c).Any(group => group.Count() == numberOfOccurrences);
        }

        bool IsFullHouse(string hand)
        {
            return IsThreeOfAKind(hand) && IsTwoOfAKind(hand);
        }

        bool IsTwoPair(string hand)
        {
            if (!IsTwoOfAKind(hand))
                return false;

            int numberOfOccurrences = 2;

            // Check if the string contains at least the specified number of occurrences of any character
            int pairs = 0;
            Hand.GroupBy(c => c).Any(group =>
            {
                if (group.Count() == numberOfOccurrences)
                    pairs++;
                return false;
            });
            //Console.WriteLine($"hand = {Hand}, paircount = {pairs}");
            return pairs > 1;
        }

        public HandType GetHandType(string hand)
        {
            if (IsFiveOfAKind(hand)) { return HandType.FiveOfAKind; }
            else if (IsFourOfAKind(hand)) { return HandType.FourOfAKind; }
            else if (IsFullHouse(hand)) { return HandType.FullHouse; }
            else if (IsThreeOfAKind(hand)) { return HandType.ThreeOfAKind; }
            else if (IsTwoPair(hand)) { return HandType.TwoPair; }
            else if (IsTwoOfAKind(hand)) { return HandType.OnePair; }

            return HandType.HighCard;
        }

        public static List<CamelCard> SortListByRank1(List<CamelCard> inputList)
        {
            // Sorting the list by the Id property
            inputList.Sort((c1, c2) =>
            {
                if (c1.Type == c2.Type)
                {
                    for (int i = 0; i < c1.Hand.Length; ++i)
                    {
                        int r1 = CardRank(c1.Hand[i]);
                        int r2 = CardRank(c2.Hand[i]);

                        if (r1 < r2)
                            return -1;
                        else if (r1 > r2)
                            return 1;
                    }

                    return 0;
                }
                else if (c1.Type <= c2.Type)
                {
                    return -1;
                }
                else
                    return 1;

            });

            return inputList;
        }
        public static List<CamelCard> SortListByRank(List<CamelCard> inputList)
        {
            // Sorting the list by the Id property
            inputList.Sort((c1, c2) =>
            {
                if (c1.Type == c2.Type)
                {
                    for (int i = 0; i < c1.Hand.Length; ++i)
                    {
                        int r1 = CardRank(c1.Hand[i]);
                        int r2 = CardRank(c2.Hand[i]);

                        if (r1 < r2)
                            return -1;
                        else if (r1 > r2)
                            return 1;
                    }

                    return 0;
                }
                else if (c1.Type <= c2.Type)
                {
                    return -1;
                }
                else
                    return 1;

            });

            return inputList;
        }

    }

    static int CardRank1(char card)
    {
        const string CardRank = "123456789TJQKA";
        return CardRank.IndexOf(card);
    }

    static void Main1()
    {
        string filePath = "../../../input.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            List<CamelCard> hands = [];

            foreach (string line in lines)
            {
                string[] data = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                hands.Add(new CamelCard(data[0], data[1]));
            }

            CamelCard.SortListByRank(hands);

            long rank = 1;
            long totalWinnings = 0;
            foreach (CamelCard hand in hands)
            {
                Console.WriteLine($"hand {hand.Hand} is rank {rank}");
                totalWinnings += (rank * hand.Bid);
                ++rank;
            }

            Console.WriteLine($"Total Winnings = {totalWinnings}");
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }

    }

    static int CardRank(char card)
    {
        const string CardRank = "J123456789TQKA";
        return CardRank.IndexOf(card);
    }

    static void Main()
    {
        string filePath = "../../../input.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            List<CamelCard> hands = [];

            foreach (string line in lines)
            {
                string[] data = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                hands.Add(new CamelCard(data[0], data[1]));
            }

            CamelCard.SortListByRank(hands);

            long rank = 1;
            long totalWinnings = 0;
            foreach (CamelCard hand in hands)
            {
                //Console.WriteLine($"hand {hand.Hand} is rank {rank}");
                totalWinnings += (rank * hand.Bid);
                ++rank;
            }

            Console.WriteLine($"Total Winnings = {totalWinnings}");
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }

    }
}

// 246474819 too high

