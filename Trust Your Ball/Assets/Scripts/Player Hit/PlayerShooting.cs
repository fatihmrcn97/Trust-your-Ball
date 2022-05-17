
using TMPro;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform bulletSpawnPos;
    public GameObject bulletPref;
    public float bulletSpeed = 10;

    public TextMeshProUGUI bulletText;

    public float timeBetweenShooting,reloadTime,timeBetweenShoots;
    public int magazineSize, bulletPerTap;

    public bool allowInvoke = true;

    int bulletsLeft, bulletsShoot;

    bool  readyToShoot, reloading;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(readyToShoot && !reloading && bulletsLeft>0)
            {
                bulletsShoot = 0;
                 Shooting();
            }
        }
     


        //if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        //    Reload();      
        //if (readyToShoot  && !reloading && bulletsLeft <= 0)
        //    Reload();
    }


    public void ShootWithBtn()
    {
        if (readyToShoot && !reloading && bulletsLeft > 0)
        {
            bulletsShoot = 0;
            Shooting();
        }
    }
     

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Start()
    {
        bulletText.SetText(bulletsLeft + " / " + magazineSize);
    }
    private void Shooting()
    {
        readyToShoot = false;
        bulletsLeft--;
        bulletsShoot++;

        var bullet = BulletPool.instance.SpawnFromPool("bul", bulletSpawnPos.position);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPos.forward * bulletSpeed, ForceMode.Impulse);

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        bulletText.SetText(bulletsLeft + " / " + magazineSize);
    }

    
    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;

    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    public void AddBullet(int bulletCount)
    {
        
        bulletsLeft += bulletCount;
        if (bulletsLeft > 14)
            bulletsLeft = 15;
        bulletText.SetText(bulletsLeft + " / " + magazineSize);
    }
}
