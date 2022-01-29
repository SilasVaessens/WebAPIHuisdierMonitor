using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebAPIHuisdierMonitor.Model;

namespace WebAPIHuisdierMonitor
{
    public class Analyse
    {
        public double MeanFoodPerDay { get; }
        public double MeanWaterPerDay { get; }
        public double MeanWeightPerDay { get; }

        public double[] MeanFoodDayPart { get; }
        public double[] MeanWaterDayPart { get; }
        public double[] MeanWeightDayPart { get; }

        public double MedianFoodPerDay { get; }
        public double MedianWaterPerDay { get; }
        public double MedianWeightPerDay { get; }

        public double[] MedianFoodDayPart { get; }
        public double[] MedianWaterDayPart { get; }
        public double[] MedianWeightDayPart { get; }

        public DateTime[] MeanFoodTimesDayPart { get; }
        public DateTime[] MeanWaterTimesDayPart { get; }
        public DateTime[] MeanWeightTimesDayPart { get; }

        public DateTime[] MedianFoodTimesDayPart { get; }
        public DateTime[] MedianWaterTimesDayPart { get; }
        public DateTime[] MedianWeightTimesDayPart { get; }

        private readonly DateTime NightToMorning = new DateTime(2000, 1, 1, 6, 0, 0);
        private readonly DateTime MorningToAfternoon = new DateTime(2000, 1, 1, 12, 0, 0);
        private readonly DateTime AfternoonToEvening = new DateTime(2000, 1, 1, 18, 0, 0);
        private readonly DateTime EveningToNight = new DateTime(2000, 1, 1, 23, 59, 59);


        public Analyse()
        {

        }

        public Analyse(List<FoodBowl> foodBowls, List<WaterBowl> waterBowls, List<PetBed> petBeds)
        {
            if (foodBowls.Count > 0 && foodBowls != null)
            {
                MeanFoodPerDay = CalculateMeanWeightPerDay(foodBowls);
                MedianFoodPerDay = CalculateMedianWeightPerDay(foodBowls);

                MeanFoodDayPart = CalculateMeanWeightDayPart(foodBowls);
                MedianFoodDayPart = CalculateMedianWeightDayPart(foodBowls);

                MeanFoodTimesDayPart = CalculateMeanTimes(foodBowls);
                MedianFoodTimesDayPart = CalculateMedianTimes(foodBowls);
            }
            if (waterBowls.Count > 0 && waterBowls != null)
            {
                MeanWaterPerDay = CalculateMeanWeightPerDay(waterBowls);
                MedianWaterPerDay = CalculateMedianWeightPerDay(waterBowls);

                MeanWaterDayPart = CalculateMeanWeightDayPart(waterBowls);
                MedianWaterDayPart = CalculateMedianWeightDayPart(waterBowls);

                MeanWaterTimesDayPart = CalculateMeanTimes(waterBowls);
                MedianWaterTimesDayPart = CalculateMedianTimes(waterBowls);
            }
            if (petBeds.Count > 0 && petBeds != null)
            {
                MeanWeightPerDay = CalculateMeanWeightPerDay(petBeds);
                MedianWeightPerDay = CalculateMedianWeightPerDay(petBeds);

                MeanWeightDayPart = CalculateMeanWeightDayPart(petBeds);
                MedianWeightDayPart = CalculateMedianWeightDayPart(petBeds);

                MeanWeightTimesDayPart = CalculateMeanTimes(petBeds);
                MedianWeightTimesDayPart = CalculateMedianTimes(petBeds);
            }
        }

        private static double CalculateMeanWeightPerDay(List<FoodBowl> DataFoodbowls)
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

        private double CalculateMeanWeightPerDay(List<WaterBowl> DataWaterBowls)
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

        private double CalculateMeanWeightPerDay(List<PetBed> DataPetBeds)
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

        private double[] CalculateMeanWeightDayPart(List<FoodBowl> DataFoodBowls)
        {
            List<FoodBowl> CleanedData = new List<FoodBowl>();
            foreach (FoodBowl foodBowl in DataFoodBowls)
            {
                if (foodBowl.Weight > 0)
                {
                    CleanedData.Add(foodBowl);
                }
            }
            return GetMeans(SortTimes(CleanedData));
        }

        private double[] CalculateMeanWeightDayPart(List<WaterBowl> DataWaterBowls)
        {
            List<WaterBowl> CleanedData = new List<WaterBowl>();
            foreach (WaterBowl waterBowl in DataWaterBowls)
            {
                if (waterBowl.Weight > 0)
                {
                    CleanedData.Add(waterBowl);
                }
            }
            return GetMeans(SortTimes(CleanedData));
        }

        private double[] CalculateMeanWeightDayPart(List<PetBed> DataPetBeds)
        {
            List<PetBed> CleanedData = new List<PetBed>();
            foreach (PetBed petBed in DataPetBeds)
            {
                if (petBed.Weight > 0)
                {
                    CleanedData.Add(petBed);
                }
            }
            return GetMeans(SortTimes(CleanedData));
        }


        private double CalculateMedianWeightPerDay(List<FoodBowl> DataFoodBowls)
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
                return CalculateMedian(Weights);
            }
            else
            {
                return 0;
            }
        }

        private double CalculateMedianWeightPerDay(List<WaterBowl> DataWaterBowls)
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
                return CalculateMedian(Weights);
            }
            else
            {
                return 0;
            }
        }


        private double CalculateMedianWeightPerDay(List<PetBed> DataPetBeds)
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
                return CalculateMedian(Weights);
            }
            else
            {
                return 0;
            }
        }

        private double[] CalculateMedianWeightDayPart(List<FoodBowl> foodBowls)
        {
            List<FoodBowl> CleanedData = new List<FoodBowl>();
            foreach (FoodBowl foodBowl in foodBowls)
            {
                if (foodBowl.Weight > 0)
                {
                    CleanedData.Add(foodBowl);
                }
            }
            return GetMedians(SortTimes(CleanedData));
        }

        private double[] CalculateMedianWeightDayPart(List<WaterBowl> waterBowls)
        {
            List<WaterBowl> CleanedData = new List<WaterBowl>();
            foreach (WaterBowl waterBowl in waterBowls)
            {
                if (waterBowl.Weight > 0)
                {
                    CleanedData.Add(waterBowl);
                }
            }
            return GetMedians(SortTimes(CleanedData));
        }

        private double[] CalculateMedianWeightDayPart(List<PetBed> petBeds)
        {
            List<PetBed> CleanedData = new List<PetBed>();
            foreach (PetBed petBed in petBeds)
            {
                if (petBed.Weight > 0)
                {
                    CleanedData.Add(petBed);
                }
            }
            return GetMedians(SortTimes(CleanedData));
        }

        private DateTime[] CalculateMeanTimes(List<FoodBowl> DataFoodBowl)
        {
            List<DateTime> Times = new List<DateTime>();

            foreach (FoodBowl Data in DataFoodBowl)
            {
                Times.Add(Data.Time);
            }
            return GetMeanTimes(SortTimes(Times));
        }

        private DateTime[] CalculateMeanTimes(List<WaterBowl> DataWaterBowl)
        {
            List<DateTime> Times = new List<DateTime>();

            foreach (WaterBowl Data in DataWaterBowl)
            {
                Times.Add(Data.Time);
            }
            return GetMeanTimes(SortTimes(Times));
        }

        private DateTime[] CalculateMeanTimes(List<PetBed> DataPetBed)
        {
            List<DateTime> Times = new List<DateTime>();

            foreach (PetBed Data in DataPetBed)
            {
                Times.Add(Data.Time);
            }
            return GetMeanTimes(SortTimes(Times));
        }


        private DateTime[] CalculateMedianTimes(List<FoodBowl> DataFoodBowls)
        {
            List<DateTime> Times = new List<DateTime>();

            foreach (FoodBowl Data in DataFoodBowls)
            {
                Times.Add(Data.Time);
            }
            return GetMedianTimes(SortTimes(Times));
        }

        private DateTime[] CalculateMedianTimes(List<WaterBowl> DataWaterBowls)
        {
            List<DateTime> Times = new List<DateTime>();

            foreach (WaterBowl Data in DataWaterBowls)
            {
                Times.Add(Data.Time);
            }
            return GetMedianTimes(SortTimes(Times));
        }

        private DateTime[] CalculateMedianTimes(List<PetBed> DataPetBeds)
        {
            List<DateTime> Times = new List<DateTime>();

            foreach (PetBed Data in DataPetBeds)
            {
                Times.Add(Data.Time);
            }
            return GetMedianTimes(SortTimes(Times));
        }

        private List<List<int>> SortTimes(List<FoodBowl> foodBowls)
        {
            List<List<int>> Day = new List<List<int>>();

            List<int> Morning = new List<int>();
            List<int> Afternoon = new List<int>();
            List<int> Evening = new List<int>();
            List<int> Night = new List<int>();

            Day.Add(Night);
            Day.Add(Morning);
            Day.Add(Afternoon);
            Day.Add(Evening);

            foreach (FoodBowl foodBowl in foodBowls)
            {
                if (foodBowl.Time.TimeOfDay >= NightToMorning.TimeOfDay && foodBowl.Time.TimeOfDay < MorningToAfternoon.TimeOfDay) //time between 6.00 and 12.00
                {
                    Morning.Add(foodBowl.Weight);
                }
                else if (foodBowl.Time.TimeOfDay >= MorningToAfternoon.TimeOfDay && foodBowl.Time.TimeOfDay < AfternoonToEvening.TimeOfDay) //time between 12.00 and 18.00
                {
                    Afternoon.Add(foodBowl.Weight);
                }
                else if (foodBowl.Time.TimeOfDay >= AfternoonToEvening.TimeOfDay && foodBowl.Time.TimeOfDay < EveningToNight.TimeOfDay) //time between 18.00 and 0.00
                {
                    Evening.Add(foodBowl.Weight);
                }
                else //time between 0.00 and 6.00
                {
                    Night.Add(foodBowl.Weight);
                }
            }
            return Day;
        }

        private List<List<float>> SortTimes(List<WaterBowl> waterBowls)
        {
            List<List<float>> Day = new List<List<float>>();

            List<float> Morning = new List<float>();
            List<float> Afternoon = new List<float>();
            List<float> Evening = new List<float>();
            List<float> Night = new List<float>();

            Day.Add(Night);
            Day.Add(Morning);
            Day.Add(Afternoon);
            Day.Add(Evening);

            foreach (WaterBowl waterBowl in waterBowls)
            {
                if (waterBowl.Time.TimeOfDay >= NightToMorning.TimeOfDay && waterBowl.Time.TimeOfDay < MorningToAfternoon.TimeOfDay) //time between 6.00 and 12.00
                {
                    Morning.Add(waterBowl.Weight);
                }
                else if (waterBowl.Time.TimeOfDay >= MorningToAfternoon.TimeOfDay && waterBowl.Time.TimeOfDay < AfternoonToEvening.TimeOfDay) //time between 12.00 and 18.00
                {
                    Afternoon.Add(waterBowl.Weight);
                }
                else if (waterBowl.Time.TimeOfDay >= AfternoonToEvening.TimeOfDay && waterBowl.Time.TimeOfDay < EveningToNight.TimeOfDay) //time between 18.00 and 0.00
                {
                    Evening.Add(waterBowl.Weight);
                }
                else //time between 0.00 and 6.00
                {
                    Night.Add(waterBowl.Weight);
                }
            }
            return Day;
        }

        private List<List<float>> SortTimes(List<PetBed> petBeds)
        {
            List<List<float>> Day = new List<List<float>>();

            List<float> Morning = new List<float>();
            List<float> Afternoon = new List<float>();
            List<float> Evening = new List<float>();
            List<float> Night = new List<float>();

            Day.Add(Night);
            Day.Add(Morning);
            Day.Add(Afternoon);
            Day.Add(Evening);

            foreach (PetBed petBed in petBeds)
            {
                if (petBed.Time.TimeOfDay >= NightToMorning.TimeOfDay && petBed.Time.TimeOfDay < MorningToAfternoon.TimeOfDay) //time between 6.00 and 12.00
                {
                    Morning.Add(petBed.Weight);
                }
                else if (petBed.Time.TimeOfDay >= MorningToAfternoon.TimeOfDay && petBed.Time.TimeOfDay < AfternoonToEvening.TimeOfDay) //time between 12.00 and 18.00
                {
                    Afternoon.Add(petBed.Weight);
                }
                else if (petBed.Time.TimeOfDay >= AfternoonToEvening.TimeOfDay && petBed.Time.TimeOfDay < EveningToNight.TimeOfDay) //time between 18.00 and 0.00
                {
                    Evening.Add(petBed.Weight);
                }
                else //time between 0.00 and 6.00
                {
                    Night.Add(petBed.Weight);
                }
            }
            return Day;
        }


        private List<List<DateTime>> SortTimes(List<DateTime> Times)
        {
            List<List<DateTime>> Day = new List<List<DateTime>>();

            List<DateTime> Morning = new List<DateTime>();
            List<DateTime> Afternoon = new List<DateTime>();
            List<DateTime> Evening = new List<DateTime>();
            List<DateTime> Night = new List<DateTime>();

            Day.Add(Night);
            Day.Add(Morning);
            Day.Add(Afternoon);
            Day.Add(Evening);

            foreach (DateTime dateTime in Times)
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
            return Day;
        }

        private double[] GetMeans(List<List<int>> Day)
        {
            double[] Means = { 0, 0, 0, 0 };

            if (Day[0].Count > 0)
            {
                Means[0] = Day[0].Average();
            }
            if (Day[1].Count > 0)
            {
                Means[1] = Day[1].Average();
            }
            if (Day[2].Count > 0)
            {
                Means[2] = Day[2].Average();
            }
            if (Day[3].Count > 0)
            {
                Means[3] = Day[3].Average();
            }
            return Means;
        }

        private double[] GetMeans(List<List<float>> Day)
        {
            double[] Means = { 0, 0, 0, 0 };

            if (Day[0].Count > 0)
            {
                Means[0] = Day[0].Average();
            }
            if (Day[1].Count > 0)
            {
                Means[1] = Day[1].Average();
            }
            if (Day[2].Count > 0)
            {
                Means[2] = Day[2].Average();
            }
            if (Day[3].Count > 0)
            {
                Means[3] = Day[3].Average();
            }
            return Means;
        }

        private double[] GetMedians(List<List<int>> Day)
        {
            double[] Median = { 0, 0, 0, 0 };

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

        private double[] GetMedians(List<List<float>> Day)
        {
            double[] Median = { 0, 0, 0, 0 };

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


        private DateTime[] GetMeanTimes(List<List<DateTime>> Day)
        {
            DateTime[] Means = { new DateTime(), new DateTime(), new DateTime(), new DateTime() };

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

        private DateTime[] GetMedianTimes(List<List<DateTime>> Day)
        {
            DateTime[] Median = { new DateTime(), new DateTime(), new DateTime(), new DateTime() };

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

        private double CalculateMedian(List<int> Weights)
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

        private double CalculateMedian(List<float> Weights)
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

        private DateTime CalculateMedian(List<DateTime> dateTimes)
        {
            DateTime dateTime = new DateTime();
            dateTimes = dateTimes.OrderBy(x => x.TimeOfDay).ToList();
            int HalfIndex = dateTimes.Count() / 2;
            if ((dateTimes.Count() % 2) == 0) //in case there is an even number of numbers, it is the average of the two middle numbers.
            {
                TimeSpan MedianTime = new TimeSpan((dateTimes[HalfIndex].TimeOfDay.Ticks + dateTimes[HalfIndex - 1].TimeOfDay.Ticks) / 2);
                return dateTime.Add(MedianTime);
            }
            else //otherwise it's just the middle number of a set of numbers
            {
                TimeSpan MedianTime = new TimeSpan(dateTimes[HalfIndex].TimeOfDay.Ticks);
                return dateTime.Add(MedianTime);
            }
        }

        private DateTime CalculateMeanOnTicksTime(List<DateTime> dateTimes)
        {
            long Ticks = 0;
            DateTime dateTime = new DateTime();
            foreach (DateTime time in dateTimes)
            {
                Ticks += time.TimeOfDay.Ticks;
            }
            TimeSpan MeanTime = new TimeSpan(Ticks / dateTimes.Count); //calculate average time that something happened
            return dateTime.Add(MeanTime);
        }

        /*onderstaande is een test, niet een eigenlijke functie
         * Deze functie is om te testen of het analyseren eigenlijk werkt met behulp van mock_data
         * Van deze mock_data wordt alleen het gewicht en de tijd gebruikt
         * Al het andere is gevuld met onzin
         */

        public Analyse Test()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            StreamReader sr1 = new StreamReader(Path.Combine(currentDirectory, "AnalyseData", "Test.json"));
            string JSON1 = sr1.ReadToEnd();
            List<FoodBowl> foodBowls = JsonConvert.DeserializeObject<List<FoodBowl>>(JSON1);

            StreamReader sr2 = new StreamReader(Path.Combine(currentDirectory, "AnalyseData", "Test2.json"));
            string JSON2 = sr2.ReadToEnd();
            List<WaterBowl> waterBowls = JsonConvert.DeserializeObject<List<WaterBowl>>(JSON2);

            StreamReader sr3 = new StreamReader(Path.Combine(currentDirectory, "AnalyseData", "Test3.json"));
            string JSON3 = sr3.ReadToEnd();
            List<PetBed> petBeds = JsonConvert.DeserializeObject<List<PetBed>>(JSON3);

            return new Analyse(foodBowls, waterBowls, petBeds);
        }
    }
}
