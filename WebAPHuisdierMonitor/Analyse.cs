using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIHuisdierMonitor.Model;

namespace WebAPIHuisdierMonitor
{
    public class Analyse
    {
        public double MeanFood { get; }
        public double MeanWater { get; }
        public double MeanWeight { get; }

        public double MedianFood { get; }
        public double MedianWater { get; }
        public double MedianWeight { get; }

        public TimeSpan[] MeanFoodTimes { get; }
        public TimeSpan[] MeanWaterTimes { get; }
        public TimeSpan[] MeanWeightTimes { get; }

        public TimeSpan[] MedianFoodTimes { get; }
        public TimeSpan[] MedianWaterTimes { get; }
        public TimeSpan[] MedianWeightTimes { get; }

        private static readonly DateTime NightToMorning = new DateTime(2000, 1, 1, 6, 0, 0);
        private static readonly DateTime MorningToAfternoon = new DateTime(2000, 1, 1, 12, 0, 0);
        private static readonly DateTime AfternoonToEvening = new DateTime(2000, 1, 1, 18, 0, 0);
        private static readonly DateTime EveningToNight = new DateTime(2000, 1, 1, 0, 0, 0);

        public Analyse(List<FoodBowl> foodBowls)
        {
            MeanFood = CalculateMeanWeight(foodBowls);
            MedianFood = CalculateMedianWeight(foodBowls);
            MeanFoodTimes = CalculateMeanTimes(foodBowls);
            MedianFoodTimes = CalculateMedianTimes(foodBowls);
        }

        public Analyse(List<WaterBowl> waterBowls)
        {
            MeanWater = CalculateMeanWeight(waterBowls);
            MedianWater = CalculateMedianWeight(waterBowls);
            MeanWaterTimes = CalculateMeanTimes(waterBowls);
            MedianWaterTimes = CalculateMedianTimes(waterBowls);
        }

        public Analyse(List<PetBed> petBeds)
        {
            MeanWeight = CalculateMeanWeight(petBeds);
            MedianWeight = CalculateMedianWeight(petBeds);
            MeanWeightTimes = CalculateMeanTimes(petBeds);
            MedianWeightTimes = CalculateMedianTimes(petBeds);
        }

        private static double CalculateMeanWeight(List<FoodBowl> DataFoodbowls)
        {
            List<int> Weights = new List<int>();
            foreach (FoodBowl foodBowl in DataFoodbowls)
            {
                if (foodBowl.Weight > 0) // filter out "bad" data
                {
                    Weights.Add(foodBowl.Weight); //get the weight data
                }
            }
            if (Weights.Count > 0 && Weights != null) //in case there is no data
            {
                return Weights.Average();
            }
            else
            {
                return 0;
            }
        }

        private double CalculateMeanWeight(List<WaterBowl> DataWaterBowls)
        {
            List<float> Weights = new List<float>();
            foreach (WaterBowl waterBowl in DataWaterBowls)
            {
                if (waterBowl.Weight > 0) // filter out "bad" data
                {
                    Weights.Add(waterBowl.Weight); //get the weight data
                }
            }
            if (Weights.Count > 0 && Weights != null) //in case there is no data
            {
                return Weights.Average();
            }
            else
            {
                return 0;
            }
        }

        private double CalculateMeanWeight(List<PetBed> DataPetBeds)
        {
            List<float> Weights = new List<float>();
            foreach (PetBed petBed in DataPetBeds)
            {
                if (petBed.Weight > 0) // filter out "bad" data
                {
                    Weights.Add(petBed.Weight); //get the weight data
                }
            }
            if (Weights.Count > 0 && Weights != null) //in case there is no data
            {
                return Weights.Average();
            }
            else
            {
                return 0;
            }
        }

        private double CalculateMedianWeight(List<FoodBowl> DataFoodBowls)
        {
            List<int> Weights = new List<int>();
            foreach (FoodBowl foodBowl in DataFoodBowls)
            {
                if (foodBowl.Weight > 0) // filter out "bad" data
                {
                    Weights.Add(foodBowl.Weight); //get the weight data
                }
            }
            if (Weights.Count > 0 && Weights != null) //in case there is no data
            {
                Weights.Sort();
                int HalfIndex = Weights.Count() / 2;
                if ((Weights.Count() % 2) == 0) //in case there is an even number of numbers, it is the average of the two middle numbers.
                {
                    return Weights[HalfIndex] + Weights[HalfIndex - 1] / 2;
                }
                else //otherwise it's just the middle number of a set of numbers
                {
                    return Weights[HalfIndex];
                }
            }
            else
            {
                return 0;
            }
        }

        private double CalculateMedianWeight(List<WaterBowl> DataWaterBowls)
        {
            List<float> Weights = new List<float>();
            foreach (WaterBowl waterBowl in DataWaterBowls)
            {
                if (waterBowl.Weight > 0) // filter out "bad" data
                {
                    Weights.Add(waterBowl.Weight); //get the weight data
                }
            }
            if (Weights.Count > 0 && Weights != null) //in case there is no data
            {
                Weights.Sort();
                int HalfIndex = Weights.Count() / 2;
                if ((Weights.Count() % 2) == 0) //in case there is an even number of numbers, it is the average of the two middle numbers.
                {
                    return Weights[HalfIndex] + Weights[HalfIndex - 1] / 2;
                }
                else //otherwise it's just the middle number of a set of numbers
                {
                    return Weights[HalfIndex];
                }
            }
            else
            {
                return 0;
            }
        }

        private double CalculateMedianWeight(List<PetBed> DataPetBeds)
        {
            List<float> Weights = new List<float>();
            foreach (PetBed petBed in DataPetBeds)
            {
                if (petBed.Weight > 0) // filter out "bad" data
                {
                    Weights.Add(petBed.Weight); //get the weight data
                }
            }
            if (Weights.Count > 0 && Weights != null) //in case there is no data
            {
                Weights.Sort();
                int HalfIndex = Weights.Count() / 2;
                if ((Weights.Count() % 2) == 0) //in case there is an even number of numbers, it is the average of the two middle numbers.
                {
                    return Weights[HalfIndex] + Weights[HalfIndex - 1] / 2;
                }
                else //otherwise it's just the middle number of a set of numbers
                {
                    return Weights[HalfIndex];
                }
            }
            else
            {
                return 0;
            }
        }

        private TimeSpan[] CalculateMeanTimes(List<FoodBowl> DataFoodBowl)
        {
            List<DateTime> Times = new List<DateTime>();

            foreach (FoodBowl Data in DataFoodBowl)
            {
                Times.Add(Data.Time);
            }
            return GetMeanTimes(SortTimes(Times));
        }

        private TimeSpan[] CalculateMeanTimes(List<WaterBowl> DataWaterBowl)
        {
            List<DateTime> Times = new List<DateTime>();

            foreach (WaterBowl Data in DataWaterBowl)
            {
                Times.Add(Data.Time);
            }
            return GetMeanTimes(SortTimes(Times));
        }

        private TimeSpan[] CalculateMeanTimes(List<PetBed> DataPetBed)
        {
            List<DateTime> Times = new List<DateTime>();

            foreach (PetBed Data in DataPetBed)
            {
                Times.Add(Data.Time);
            }
            return GetMeanTimes(SortTimes(Times));
        }


        private TimeSpan[] CalculateMedianTimes(List<FoodBowl> DataFoodBowls)
        {
            List<DateTime> Times = new List<DateTime>();

            foreach (FoodBowl Data in DataFoodBowls)
            {
                Times.Add(Data.Time);
            }
            return GetMedianTimes(SortTimes(Times));
        }

        private TimeSpan[] CalculateMedianTimes(List<WaterBowl> DataWaterBowls)
        {
            List<DateTime> Times = new List<DateTime>();

            foreach (WaterBowl Data in DataWaterBowls)
            {
                Times.Add(Data.Time);
            }
            return GetMedianTimes(SortTimes(Times));
        }

        private TimeSpan[] CalculateMedianTimes(List<PetBed> DataPetBeds)
        {
            List<DateTime> Times = new List<DateTime>();

            foreach (PetBed Data in DataPetBeds)
            {
                Times.Add(Data.Time);
            }
            return GetMedianTimes(SortTimes(Times));
        }


        private List<List<DateTime>> SortTimes(List<DateTime> Times)
        {
            List<List<DateTime>> Day = new List<List<DateTime>>();

            List<DateTime> Morning = new List<DateTime>();
            List<DateTime> Afternoon = new List<DateTime>();
            List<DateTime> Evening = new List<DateTime>();
            List<DateTime> Night = new List<DateTime>();

            Day.Add(Morning);
            Day.Add(Afternoon);
            Day.Add(Evening);
            Day.Add(Night);

            foreach (DateTime dateTime in Times)
            {
                if (DateTime.Compare(new DateTime(), dateTime) > 0)
                {
                    if (dateTime.TimeOfDay >= NightToMorning.TimeOfDay && dateTime.TimeOfDay < MorningToAfternoon.TimeOfDay) //time between 6.00 and 12.00
                    {
                        Morning.Add(dateTime);
                    }
                    else if (dateTime.TimeOfDay >= MorningToAfternoon.TimeOfDay && dateTime.TimeOfDay < AfternoonToEvening.TimeOfDay) //time between 12.00 and 18.00
                    {
                        Afternoon.Add(dateTime);
                    }
                    else if (dateTime.TimeOfDay >= AfternoonToEvening.TimeOfDay && dateTime.TimeOfDay < EveningToNight.TimeOfDay) //time between 18.00 and 0.00
                    {
                        Evening.Add(dateTime);
                    }
                    else //time between 0.00 and 6.00
                    {
                        Night.Add(dateTime);
                    }
                }
            }
            return Day;
        }

        private TimeSpan[] GetMeanTimes(List<List<DateTime>> Day)
        {
            TimeSpan[] Means = { new TimeSpan(), new TimeSpan(), new TimeSpan(), new TimeSpan() };

            if (Day[0].Count > 0)
            {
                Means[0] = CalculateMeanOnTicksTime(Day[0]);
            }
            if (Day[1].Count > 0)
            {
                Means[1] = CalculateMeanOnTicksTime(Day[1]);
            }
            if (Day[2].Count > 0)
            {
                Means[2] = CalculateMeanOnTicksTime(Day[2]);
            }
            if (Day[3].Count > 0)
            {
                Means[3] = CalculateMeanOnTicksTime(Day[3]);
            }
            return Means;
        }

        private TimeSpan[] GetMedianTimes(List<List<DateTime>> Day)
        {
            TimeSpan[] Median = { new TimeSpan(), new TimeSpan(), new TimeSpan(), new TimeSpan() };

            if (Day[0].Count > 0)
            {
                Median[0] = CalculateMedian(Day[0]);
            }
            if (Day[1].Count > 0)
            {
                Median[1] = CalculateMedian(Day[1]);
            }
            if (Day[2].Count > 0)
            {
                Median[2] = CalculateMedian(Day[2]);
            }
            if (Day[3].Count > 0)
            {
                Median[3] = CalculateMedian(Day[3]);
            }
            return Median;
        }

        private TimeSpan CalculateMedian(List<DateTime> dateTimes)
        {
            dateTimes = dateTimes.OrderBy(x => x.TimeOfDay).ToList();
            int HalfIndex = dateTimes.Count() / 2;
            if ((dateTimes.Count() % 2) == 0) //in case there is an even number of numbers, it is the average of the two middle numbers.
            {
                return new TimeSpan((dateTimes[HalfIndex].TimeOfDay.Ticks + dateTimes[HalfIndex - 1].TimeOfDay.Ticks) / 2);
            }
            else //otherwise it's just the middle number of a set of numbers
            {
                return new TimeSpan(dateTimes[HalfIndex].TimeOfDay.Ticks);
            }
        }

        private TimeSpan CalculateMeanOnTicksTime(List<DateTime> dateTimes)
        {
            long Ticks = 0;
            foreach (DateTime time in dateTimes)
            {
                Ticks += time.TimeOfDay.Ticks;
            }
            TimeSpan MeanTime = new TimeSpan(Ticks / dateTimes.Count); //calculate average time that something happened
            return MeanTime;
        }
    }
}
