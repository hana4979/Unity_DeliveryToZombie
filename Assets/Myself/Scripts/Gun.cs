using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    // ���� ���¸� ǥ���ϴµ� ����� Ÿ�� ����
    public enum State
    {
        Ready, // �߻� �غ� ��
        Empty, // źâ�� ��
        Reloading // ������ ��
    }

    public State state { get; private set; } // ���� ���� ����

    [Header ("# GunData")]
    public AudioClip shotClip; // �߻� �Ҹ�
    public AudioClip reloadClip; // ������ �Ҹ�
    public float damage = 10; // ���ݷ�
    [Header ("# Ammo")]
    public int startAmmoRemain = 25; // ó���� �־��� ��ü ź��
    public int ammoRemain = 25; // ���� ��ü ź��
    public int magAmmo; // ���� źâ�� ���� �ִ� ź��
    public int magCapacity = 25; // źâ �뷮
    [Header ("# ShotData")]
    public float timeBetFire = 0.12f; // ź�� �߻� ����
    public float reloadTime = 1.8f; // ������ �ҿ� �ð�
    private float fireDistance = 50f; // �����Ÿ�
    private float lastFireTime; // ���� ���������� �߻��� ����
    public Transform fireTransform; // ź���� �߻�� ��ġ

    private LineRenderer bulletLineRenderer; // ź�� ������ �׸��� ���� ������
    private AudioSource gunAudioPlayer; // �� �Ҹ� �����

    private void Awake()
    {
        // ����� ������Ʈ�� ���� ��������
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        // ����� ���� �� ���� ����
        bulletLineRenderer.positionCount = 2;
        // ���� �������� ��Ȱ��ȭ
        bulletLineRenderer.enabled = false;
    }

    private void OnEnable()
    {
        // ��ü ���� ź�� ���� �ʱ�ȭ
        ammoRemain = startAmmoRemain;
        // ���� źâ ���� ä���
        magAmmo = magCapacity;

        // ���� ���� ���¸� ���� �� �غ� �� ���·� ����
        state = State.Ready;
        // ���������� ���� �� ������ �ʱ�ȭ
        lastFireTime = 0;
    }

    // �߻� �õ�
    public void Fire()
    {
        // ���� ���°� �߻� ������ ����
        // && ������ �� �߻� �������� gunData.timeBetFire �̻��� �ð��� ����
        if(state == State.Ready && Time.time >= lastFireTime + timeBetFire)
        {
            // ������ �� �߻� ���� ����
            lastFireTime = Time.time;
            // ���� �߻� ó�� ����
            Shot();
        }
    }

    // ���� �߻� ó��
    private void Shot()
    {
        
        // ����ĳ��Ʈ�� ���� �浹 ������ �����ϴ� �����̳�
        RaycastHit hit;
        // ź���� ���� ���� ������ ����
        Vector3 hitPosition = Vector3.zero;

        // ����ĳ��Ʈ(���� ����, ����, �浹 ���� �����̳�, �����Ÿ�)
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            // ���̰� � ��ü�� �浹�� ���

            // �浹�� �������κ��� IDamageable ������Ʈ �������� �õ�
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            // �������κ��� IDamageable ������Ʈ �������� �� �����ߴٸ�
            if (target != null)
            {
                // ������ OnDamage �Լ��� ������� ���濡�� ����� �ֱ�
                target.OnDamage(damage, hit.point, hit.normal);
            }

            // ���̰� �浹�� ��ġ ����
            hitPosition = hit.point;
        }
        else
        {
            // ���̰� �ٸ� ��ü�� �浹���� �ʾҴٸ�
            // ź���� �ִ� �����Ÿ����� ���ư��� ���� ��ġ�� �浹 ��ġ�� ���
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }

        // �߻� ����Ʈ ��� ����
        StartCoroutine(ShotEffect(hitPosition));

        // ���� ź�� ���� -1
        magAmmo--;
        UIManager.instance.UpdateAmmoText(magAmmo, magCapacity);

        if (magAmmo <= 0)
        {
            // źâ�� ���� ź���� ���ٸ� ���� ���� ���¸� Empty�� ����
            state = State.Empty;
        }
    }

    // �߻� ����Ʈ�� �Ҹ��� ����ϰ� ź�� ������ �׸�
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        // �Ѱ� �Ҹ� ���
        gunAudioPlayer.PlayOneShot(shotClip);

        // ���� �������� �ѱ��� ��ġ
        bulletLineRenderer.SetPosition(0, fireTransform.position);
        // ���� ������ �Է����� ���� �浹 ��ġ
        bulletLineRenderer.SetPosition(1, hitPosition);
        // ���� �������� Ȱ��ȭ�Ͽ� ź�� ������ �׸�
        bulletLineRenderer.enabled = true;

        // 0.03�� ���� ��� ó���� ���
        yield return new WaitForSeconds(0.03f);

        // ���� �������� ��Ȱ��ȭ�Ͽ� ź�� ������ ����
        bulletLineRenderer.enabled = false;
    }

    // ������ �õ�
    public bool Reload()
    {
        if(state == State.Reloading || magAmmo >= magCapacity)
        {
            // �̹� ������ ���̰ų� (���� ź���� ���ų�)
            // źâ�� ź���� �̹� ������ ��� �������� �� ����
            return false;
        }

        // ������ ó�� ����
        StartCoroutine(ReloadRoutine());
        return true;
    }

    // ���� ������ ó���� ����
    private IEnumerator ReloadRoutine()
    {
        // ���� ���¸� ������ �� ���·� ��ȯ
        state = State.Reloading;
        // ������ �Ҹ� ���
        gunAudioPlayer.PlayOneShot(reloadClip);

        // ������ �ҿ� �ð� ��ŭ ó�� ����
        yield return new WaitForSeconds(reloadTime);

        /*
        // źâ�� ä�� ź�� ���
        int ammoToFill = magCapacity - magAmmo;

        // źâ�� ä���� �� ź���� ���� ź�˺��� ���ٸ�
        // ä���� �� ź�� ���� ���� ź�� ���� ���� ����
        if(ammoRemain < ammoToFill)
        {
            ammoToFill = ammoRemain;
        }
        */

        // źâ�� ä��
        magAmmo = magCapacity;

        UIManager.instance.UpdateAmmoText(magAmmo, magCapacity);

        /*
        // ���� ź�˿��� źâ�� ä�ŭ ź���� ��
        ammoRemain -= ammoToFill;
        */

        // ���� ���� ���¸� �߻� �غ�� ���·� ����
        state = State.Ready;
    }
}
