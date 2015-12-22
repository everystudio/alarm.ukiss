using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RakutenBilling : Singleton<RakutenBilling> , AppMarketHelperCallback{
	AppMarketHelper mAppMarketHelper;
	// Use this for initialization
	void Start () {
		Debug.Log ("RakutenBilling.Start");
		mAppMarketHelper = gameObject.GetComponent<AppMarketHelper>();
		Setup("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAt1zV9nk8ickTYZ3kkbkiMoW8zVvp0hTj9tGVja5m1oDiyYWzeKHXkWpETxwAMt7AOeof/rsDvDKNlgTqUtrPAvc+2yx4WsbauXXpST8+j4MLcsmjqerlMI8Nbyj1a0hScnRDfpyk7XFYlSXBbIewwxxMKM/qSba9nRQmVdaGvRdbV6YPD2ryKSZ5TOUJUL4oT9SPiTiQszc5VUr1xUGsoRhakzfX2VtnoLnrdzy8gQDZ5WQLheAY0cH79isQsq5Y08+LtAhPEvx8CKMTuQ5iirqXSANWwO9Dh70lz903nY2MoEb2gvMwWOEVI4aZdDFRMJXN3ZuxDXnqCShMYZyLLwIDAQAB" );
	}

	public void Setup( string _strKey ){
		Debug.LogError ("call:Setup");
		//初期設定の呼び出し
		mAppMarketHelper.SetUp (this, _strKey);
	}

	public void startQueryInventory(){
		mAppMarketHelper.QueryInventory();
	}

	public void purchaseProduct( string _strProductId ){
		mAppMarketHelper.LaunchPurchaseFlow (_strProductId, "payload");
	}

	// callbackとか
	public void OnAppMarketSetupFinished(){
		/*
		RakutenBilling.Instance.Setup ( "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAt1zV9nk8ickTYZ3kkbkiMoW8zVvp0hTj9tGVja5m1oDiyYWzeKHXkWpETxwAMt7AOeof/rsDvDKNlgTqUtrPAvc+2yx4WsbauXXpST8+j4MLcsmjqerlMI8Nbyj1a0hScnRDfpyk7XFYlSXBbIewwxxMKM/qSba9nRQmVdaGvRdbV6YPD2ryKSZ5TOUJUL4oT9SPiTiQszc5VUr1xUGsoRhakzfX2VtnoLnrdzy8gQDZ5WQLheAY0cH79isQsq5Y08+LtAhPEvx8CKMTuQ5iirqXSANWwO9Dh70lz903nY2MoEb2gvMwWOEVI4aZdDFRMJXN3ZuxDXnqCShMYZyLLwIDAQAB" );
		*/
		Debug.Log ("OnAppMarketSetupFinished");
		mAppMarketHelper.InitBilling();
	}



	public void OnAppMarketError(string action, string message){
		Debug.Log ( string.Format( "OnAppMarketError action:{0} message:{1}" , action , message));
		// 失敗したらほっておく
	}

	public void OnInitBIllingFinished(IabResult result){
		Debug.Log ("OnInitBIllingFinished:"+result.ToString());

		if (result.Response == AppMarketHelper.BILLING_RESPONSE_RESULT_OK) {
			RakutenBilling.Instance.startQueryInventory ();
			return;
		}



	}

	public void OnIabQueryInventoryFinished(IabResult result, IabInventory inventory){

		if (result.Response == AppMarketHelper.BILLING_RESPONSE_RESULT_OK) {

			List<IabPurchase> purchase_list = inventory.getAllPurchases();
			DataManagerAlarm.Instance.purchased_list.Clear ();
			foreach (IabPurchase purchase in purchase_list) {
				DataManagerAlarm.Instance.purchased_list.Add (purchase.Sku);
				//Debug.LogError( string.Format( "productId:{0}" ,purchase.productId ));
			}
		}
		return;
	}
	public void OnIabQuerySkuDetailsFinished(IabResult result, Dictionary<string, IabSkuDetails> skuDetailsDictionary){
	}

	public void OnIabPurchaseFinished(IabResult result, IabPurchase purchase){
		DataManagerAlarm.Instance.purchased_list.Add (purchase.Sku);
		GameMain.Instance.Purchase (purchase.Sku);
	}

	public void OnIabSubscriptionPurchaseFinished(IabResult result, IabPurchase purchase){
	}
	public void OnIabConsumeFinished(IabResult result, IabPurchase purchase){
	}

	public void OnInitLicenseFinished(){
	}
	public void OnLicenseCheckFinished(bool isSucceed, bool isLicensed){
	}


}
