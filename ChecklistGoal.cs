using System;

namespace EternalQuest
{
    public class ChecklistGoal : Goal
    {
        private int _amountCompleted;
        private int _target;
        private int _bonus;

        public ChecklistGoal(string name, string description, int points, int target, int bonus, int amountCompleted = 0)
            : base(name, description, points)
        {
            _target = target;
            _bonus = bonus;
            _amountCompleted = amountCompleted;
        }

        public override int RecordEvent()
        {
            if (IsComplete())
            {
                Console.WriteLine("Checklist already completed. No additional points.");
                return 0;
            }

            _amountCompleted++;
            int awarded = _points;
            if (IsComplete())
            {
                awarded += _bonus;
                Console.WriteLine($"Checklist complete! Bonus {_bonus} points awarded.");
            }
            return awarded;
        }

        public override bool IsComplete() => _amountCompleted >= _target;

        public override string GetDetailsString()
        {
            string status = IsComplete() ? "[X]" : "[ ]";
            return $"{status} {_shortName} ({_description}) -- Completed {_amountCompleted}/{_target} times";
        }

        public override string GetStringRepresentation()
        {
            // Type|name,description,points,target,bonus,amountCompleted
            return $"ChecklistGoal|{_shortName},{_description},{_points},{_target},{_bonus},{_amountCompleted}";
        }

        public static ChecklistGoal FromSaveString(string payload)
        {
            var p = payload.Split(',');
            string name = p[0];
            string desc = p[1];
            int points = int.Parse(p[2]);
            int target = int.Parse(p[3]);
            int bonus = int.Parse(p[4]);
            int amountCompleted = int.Parse(p[5]);
            return new ChecklistGoal(name, desc, points, target, bonus, amountCompleted);
        }
    }
}
