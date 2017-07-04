namespace DungeonsOfDoom
{
    class StaminaPotion : Potion
    {
        public StaminaPotion(int weight, int strength) : base("Stamina Potion", weight, strength)
        {

        }

        public override void Use(Player player)
        {
            player.Health += Strength;
        }
    }
}