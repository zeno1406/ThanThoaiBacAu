public class FirstFight
{
    private int initialHP;
    private int currentHP;
    private int currentDrachma;
    private int blueFragments;
    private int redFragments;
    private int totalFragmentValue;
    private bool hasZeusArmor;
    private bool hasZeusShield;
    private bool hasMetAthena;
    private int titanDefeatCount;
    private int monstersDefeatedWithTitan;
    private int mainResult;
    private List<int> blueFragmentValues;
    private List<int> redFragmentValues;
    private List<int> events;
    private int R, N, ID, M;

    // Constructor
    public FirstFight()
    {
        blueFragmentValues = new List<int>();
        redFragmentValues = new List<int>();
    }

    // Khởi tạo các giá trị ban đầu
    public void Initialize(int r, int n, int id, int m, List<int> e)
    {
        R = r;
        N = n;
        ID = id;
        M = m;
        events = e;
        
        // Khởi tạo HP ban đầu dựa trên ID anh hùng
        switch (ID)
        {
            case 1: // Perseus
                initialHP = 999;
                break;
            case 2: // Theseus
                initialHP = 900;
                break;
            case 3: // Odysseus
                initialHP = 888;
                break;
            case 4: // Hercules
                initialHP = 777;
                break;
        }
        
        // Khởi tạo các giá trị khác
        currentHP = initialHP;
        currentDrachma = M;
        blueFragments = 0;
        redFragments = 0;
        totalFragmentValue = 0;
        hasZeusArmor = false;
        hasZeusShield = false;
        hasMetAthena = false;
        titanDefeatCount = 0;
        monstersDefeatedWithTitan = 0;
        mainResult = 0;
    }

    // Xử lý các sự kiện
    public int Process()
    {
        for (int i = 0; i < events.Count; i++)
        {
            int event_code = events[i];
            
            // Lấy mã sự kiện
            int eventType = event_code / 100;
            int XY = event_code % 100;
            
            // Xử lý từng loại sự kiện
            switch (eventType)
            {
                case 1: // Tìm thấy mảnh thiên thạch màu xanh
                    ProcessBlueFragment(XY);
                    break;
                    
                case 2: // Tìm thấy mảnh thiên thạch màu đỏ
                    ProcessRedFragment(XY);
                    break;
                    
                case 3: // Đến đền thờ của thần Asclepius
                    ProcessAsclepiusTemple(XY);
                    break;
                    
                case 4: // Gặp quái vật của thần Ares
                    if (ID == 3 && monstersDefeatedWithTitan > 0)
                    {
                        monstersDefeatedWithTitan--;
                    }
                    else
                    {
                        ProcessAresMonster(XY);
                    }

                    if (hasZeusShield && blueFragments < redFragments)
                    {
                        if (i + 1 < events.Count)
                        {
                            events.Insert(i + 1, event_code);
                        }
                        hasZeusShield = false;
                    }
                    break;
                    
                case 5: // Gặp nữ thần Athena
                    if (!hasMetAthena)
                    {
                        ProcessAthena(XY);
                        hasMetAthena = true;
                    }
                    break;
                    
                case 6: // Nhặt được lá chắn của thần Zeus
                    ProcessZeusShield(XY);
                    break;
                    
                case 7: // Gặp Titan
                    if (ID == 3)
                    {
                        monstersDefeatedWithTitan = 3;
                    }
                    else
                    {
                        ProcessTitan(XY);
                    }

                    if (hasZeusShield && blueFragments < redFragments)
                    {
                        if (i + 1 < events.Count)
                        {
                            events.Insert(i + 1, event_code);
                        }
                        hasZeusShield = false;
                    }
                    break;
                    
                case 8: // Gặp nữ thần Hera
                    ProcessHera(XY);
                    break;
                    
                case 9: // Gặp thần Hermes
                    ProcessHermes(i + 1, XY);
                    break;
            }
            
            // Kiểm tra các điều kiện kết thúc
            if (CheckEndConditions())
            {
                return mainResult;
            }
        }
        
        // Trường hợp 4: Đã đi hết sự kiện nhưng chưa tìm đủ mảnh thiên thạch
        if (blueFragments + redFragments < N)
        {
            mainResult = 0;
        }
        
        return mainResult;
    }

    // Xử lý mảnh thiên thạch màu xanh
    private void ProcessBlueFragment(int value)
    {
        if (ID == 3) // Odysseus bán ngay mảnh thiên thạch xanh
        {
            currentDrachma += value;
            if (currentDrachma > 999)
            {
                currentDrachma = 999; // Giới hạn tối đa là 999 drachma
            }
        }
        else
        {
            blueFragments++;
            blueFragmentValues.Add(value);
            totalFragmentValue += value;
        }

        // Kiểm tra xem đã tìm đủ mảnh thiên thạch hay chưa
        if (blueFragments + redFragments >= N)
        {
            mainResult = currentHP + currentDrachma + totalFragmentValue;
        }
    }

    // Xử lý mảnh thiên thạch màu đỏ
    private void ProcessRedFragment(int value)
    {
        redFragments++;
        redFragmentValues.Add(value);
        totalFragmentValue += value;

        // Kiểm tra xem đã tìm đủ mảnh thiên thạch hay chưa
        if (blueFragments + redFragments >= N)
        {
            mainResult = currentHP + currentDrachma + totalFragmentValue;
        }
    }

    // Xử lý đền thờ của thần Asclepius
    private void ProcessAsclepiusTemple(int XY)
    {
        int maxHeal = XY;
        int hpNeeded = initialHP - currentHP;

        if (ID == 4) // Hercules phải trả phụ phí
        {
            if (R < 3 || currentDrachma <= 2)
            {
                return;
            }
            
            int P = FindLargestPrimeLessThanR();
            if (P <= 0)
            {
                return;
            }
            
            int maxHealPossible = Math.Min(hpNeeded, currentDrachma / P);
            maxHealPossible = Math.Min(maxHealPossible, maxHeal);
            
            currentHP += maxHealPossible;
            currentDrachma -= maxHealPossible * P;
        }
        else
        {
            int healAmount = Math.Min(Math.Min(hpNeeded, currentDrachma), maxHeal);
            currentHP += healAmount;
            currentDrachma -= healAmount;
        }
    }

    // Tìm số nguyên tố lớn nhất nhỏ hơn R
    private int FindLargestPrimeLessThanR()
    {
        for (int i = R - 1; i >= 2; i--)
        {
            if (IsPrime(i))
            {
                return i;
            }
        }
        return 0;
    }

    // Kiểm tra số nguyên tố
    private bool IsPrime(int n)
    {
        if (n <= 1) return false;
        if (n <= 3) return true;
        if (n % 2 == 0 || n % 3 == 0) return false;

        for (int i = 5; i * i <= n; i += 6)
        {
            if (n % i == 0 || n % (i + 2) == 0)
            {
                return false;
            }
        }
        return true;
    }

    // Xử lý quái vật của thần Ares
    private void ProcessAresMonster(int XY)
    {
        int h1 = XY * R;
        int h2 = (currentHP + h1) % (100 + R);
        
        if (ID == 2) // Theseus
        {
            if (IsPrime(h2) && h2 > R)
            {
                currentHP -= XY;
            }
        }
        else if (ID == 4) // Hercules
        {
            if (currentHP - XY <= 0)
            {
                return;
            }

            if (currentHP >= h1)
            {
                currentDrachma += XY;
                if (currentDrachma > 999)
                {
                    currentDrachma = 999;
                }

                if (h2 == 0)
                {
                    if (hasZeusArmor)
                    {
                        double alpha = Math.Pow((R + 1.0) / 2.0, R);
                        int damage = (int)Math.Round(XY * alpha);
                        currentHP -= damage;
                    }
                    else
                    {
                        currentHP -= XY;
                    }
                }
            }
            else
            {
                if (hasZeusArmor)
                {
                    double alpha = Math.Pow((R + 1.0) / 2.0, R);
                    int damage = (int)Math.Round(XY * alpha);
                    currentHP -= damage;
                }
                else
                {
                    currentHP -= XY;
                }
            }
        }
        else
        {
            if (currentHP >= h1)
            {
                currentDrachma += XY;
                if (currentDrachma > 999)
                {
                    currentDrachma = 999;
                }

                if (h2 == 0)
                {
                    if (hasZeusArmor)
                    {
                        double alpha = Math.Pow((R + 1.0) / 2.0, R);
                        int damage = (int)Math.Round(XY * alpha);
                        currentHP -= damage;
                    }
                    else
                    {
                        currentHP -= XY;
                    }
                }
            }
            else
            {
                if (hasZeusArmor)
                {
                    double alpha = Math.Pow((R + 1.0) / 2.0, R);
                    int damage = (int)Math.Round(XY * alpha);
                    currentHP -= damage;
                }
                else
                {
                    currentHP -= XY;
                }
            }
        }
    }

    // Xử lý khi gặp nữ thần Athena
    private void ProcessAthena(int XY)
    {
        int h1 = XY * R;

        if (currentHP >= h1)
        {
            if (ID == 3)
            {
                ProcessRedFragment(XY);
            }
            else
            {
                ProcessBlueFragment(XY);
            }

            if (GCD(blueFragments, redFragments) == 1 && blueFragments > 0 && redFragments > 0)
            {
                hasZeusArmor = true;
            }
        }
        else
        {
            currentDrachma /= 2;
            currentHP -= XY;
        }
    }

    // Tìm ước chung lớn nhất
    private int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    // Xử lý khi nhặt được lá chắn của thần Zeus
    private void ProcessZeusShield(int XY)
    {
        hasZeusShield = true;

        if (blueFragments >= redFragments)
        {
            currentHP += XY;
            if (currentHP > initialHP)
            {
                currentHP = initialHP;
            }
        }
    }

    // Xử lý khi gặp Titan
    private void ProcessTitan(int XY)
    {
        if (ID == 4 && currentHP - XY <= 0)
        {
            return;
        }

        int h1 = XY * R;

        if (currentHP >= h1)
        {
            titanDefeatCount++;

            if (titanDefeatCount >= 3)
            {
                mainResult = currentHP + currentDrachma + totalFragmentValue;
            }
        }
        else
        {
            if (hasZeusArmor)
            {
                double alpha = Math.Pow((R + 1.0) / 2.0, R);
                int damage = (int)Math.Round(XY * alpha);
                currentHP -= damage;
            }
            else
            {
                currentHP -= XY;
            }
        }
    }

    // Xử lý khi gặp nữ thần Hera
    private void ProcessHera(int XY)
    {
        int X = XY / 10;
        int Y = XY % 10;

        if (ID == 1)
        {
            mainResult = -1;
            return;
        }
        else if (ID == 3)
        {
            mainResult = currentHP + currentDrachma + totalFragmentValue;
        }
    }

    // Xử lý khi gặp thần Hermes
    private void ProcessHermes(int i, int XY)
    {
        mainResult = XY;
    }

    // Kiểm tra điều kiện kết thúc
    private bool CheckEndConditions()
    {
        return mainResult != 0;
    }
    public static int ProcessGame(InputData input)
    {
        var fight = new FirstFight();
        fight.Initialize(input.R, input.N, input.ID, input.M, input.Events);
        return fight.Process();
    }
}


