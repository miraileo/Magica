    
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];
        public float lvl;
        public int amountOfLvlForNextLevel = 20;
        public int _maxAmountOfSpheres = 3;
        public bool newPlayer = true;
        public bool quasScene;
        public bool exortScene;
        public bool wexScene;
        public float _maxHealth;
        public float _score;
        public float numOfWave = 0;
        public float _maxPlayerHealth = 100;
        public float _maxPlayerMana = 100;
        public float playerDamage = 20;
        public float healthRegen = 1;
        public float manaRegen = 2;
        public float iceFieldDamage = 20;
        public bool blueWandWasBied = false;
        public bool blueWandBonusGotten = false;
        public bool purpleWandBonusGotten = false;
        public bool purpleWandWasBied = false;
        public float blastDamage = 20;
        public float meteorFieldDamage = 40;
        public bool redWandWasBied = false;
        public bool redWandBonusGotten = false;
        public int coins = 0;
        public int numOfElementals = 0;
        /*        public float enemyMaxHealth = 100;
                public float enemyDamage = 20;*/



        // Ваши сохранения

        // ...

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны
        // Пока выявленное ограничение - это расширение массива


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;

            // Длина массива в проекте должна быть задана один раз!
            // Если после публикации игры изменить длину массива, то после обновления игры у пользователей сохранения могут поломаться
            // Если всё же необходимо увеличить длину массива, сдвиньте данное поле массива в самую нижнюю строку кода
        }
    }
}
