using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectibles
{
    float _collectDuration = 0.5f;
    [SerializeField] float _collectHeight = 3f;
    [SerializeField] float _collectLength = 3f;
    [SerializeField] float _addCoinDuration = 0.6f;
    Color _spriteColor;
    public override void OnCollected(Collider2D col)
    {
        StartCoroutine(CollectedAnimation(col.transform));
    }
    IEnumerator CollectedAnimation(Transform targetTrans)
    {   
        SpriteRenderer spriteRenderer = transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>();
        _spriteColor.r = 0.05f;
        spriteRenderer.color = _spriteColor;

        Vector2 startPosition = transform.position;
        Vector2 direction = (transform.position - targetTrans.position).normalized;
        float t = 0;

        Singleton.Instance.Audio.PlaySound(Singleton.Instance.Game.CoinSFX);
        while(t <= 1)
        {
            t += Time.unscaledDeltaTime/_collectDuration;
            transform.position = Vector3.Lerp(startPosition, targetTrans.position, t);
            ShadowTrans.localPosition = new Vector2(SkinTrans.localPosition.x, 0);
            // SkinTrans.localPosition = direction * xCurve.Evaluate(t)*_collectLength;
            // SkinTrans.localPosition += Vector3.up * yCurve.Evaluate(t)*_collectHeight;
            SkinTrans.localPosition = direction * CoinCollectCurve(1-t, 3) *_collectLength;
            SkinTrans.localPosition += Vector3.up * CoinCollectCurve(t, 3)*_collectHeight;

            yield return null;
        }
        transform.position = targetTrans.position;
        ShadowTrans.gameObject.SetActive(false);

        Transform coinCollectPoint = Singleton.Instance.Game.CoinCollectPoint;
        startPosition = transform.position;
        t = 0;
        while(t <= 1)
        {
            t += Time.unscaledDeltaTime/_addCoinDuration;
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(coinCollectPoint.position);
            transform.position = Vector3.Slerp(startPosition, targetPosition, Ease.OutQuad(t));
            yield return null;
        }
        Singleton.Instance.Game.AddCoins(Value);
        Singleton.Instance.Game.CollectiblesManager.DestroyCoins(this);
        // gameObject.SetActive(false);
    }

    [SerializeField] AnimationCurve xCurve;
    [SerializeField] AnimationCurve yCurve;
    float CoinCollectCurve(float x, float pow)
    {   
        // float p = 2/3f;
        float a = -6.75f;//1/(p*p*p - p*p);
        return a * Mathf.Pow(x, pow) - a * Mathf.Pow(x, pow * 2/3);
        // return a * x*x*x - a * x*x;
    }

    public void OnSpawn() => StartCoroutine(SpawnAnimation());
    IEnumerator SpawnAnimation()
    {   
        SpriteRenderer spriteRenderer = transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>();

        _spriteColor = Random.ColorHSV();
        _spriteColor.r = 0.05f; //not isJumping
        _spriteColor.g = Random.Range(0.05f, 1f); //Random flipbook offset
        _spriteColor.b = Random.Range(0.05f, 1f); //Random Jump offset
        _spriteColor.a = 1;
        spriteRenderer.color = _spriteColor;

        Vector2 randomDirection = Random.insideUnitCircle;

        Vector2 startPosition = transform.position;
        Vector2 targetPosition = (Vector2)transform.position + randomDirection * Random.Range(0.5f, 6f);

        float height = Random.Range(2f, 6f);
        float duration = Random.Range(0.3f, 0.8f);

        ShadowTrans.gameObject.SetActive(true);
        GetComponent<Collider2D>().enabled = false;
        float t = 0;
        while(t <= 1)
        {
            t += Time.deltaTime/duration;
            transform.position = Vector2.Lerp(startPosition, targetPosition, t);
            SkinTrans.localPosition = new Vector2(0, NormalizedParabole(t) * height);
            yield return null;
        }
        SkinTrans.localPosition = Vector2.zero;

        _spriteColor.r = 1f; //isJumping
        spriteRenderer.color = _spriteColor;


        yield return new WaitForSeconds(0.5f);
        GetComponent<Collider2D>().enabled = true;
    }

    
}
