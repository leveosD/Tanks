mergeInto(LibraryManager.library, {
	
	Ready: function() {
		try {
			if(ysdk) {
				ysdk.features.LoadingAPI.ready();
				SendMessage('Yandex', 'SetReady');
				console.log("YaGames is ready!");
			}
		} catch(e) { console.log('Loading API error: ', e); }
	},
	
	GameplayStart: function() {
		if(ysdk.features.GameplayAPI) {
			ysdk.features.GameplayAPI.start();
		}
	},
	
	GameplayStop: function() {
		if(ysdk.features.GameplayAPI) {
			ysdk.features.GameplayAPI.stop();
		}
	},
	
	GetPlayerInfo: function() {
		try{
			var device = window.ysdk.deviceInfo.type;
			SendMessage('Yandex', 'SetDeviceType', device);
			
			console.log("player info is got");
		} catch(error){
			console.log(error);
		}
	},
	
	RateGame: function() {
		ysdk.feedback.canReview()
			.then(({ value, reason }) => {
				if (value) {
					ysdk.feedback.requestReview()
						.then(({ feedbackSent }) => {
							console.log(feedbackSent);
						})
				} else {
					console.log(reason)
				}
			});
	},
	
	SaveRecord: function(rec) {
		console.log("Save function JS");
		player.setStats({record: rec}).then(() => {
			console.log('data is set');
		}).catch((error) => {
			console.log(error);
		});
	},
	
	LoadRecord: function(){
		try{
			player.getStats(['record']).then((_rec) => {
				const myJSON = JSON.stringify(_rec);
				console.log(myJSON);
				SendMessage('Yandex', 'SetRecord', myJSON);
			})
		} catch(error){
			console.log(error);
		}
	},
	
	UpdateResolution: function(){
		var canvas = document.querySelector("#unity-canvas");
		canvas.style.width = "100%";
        canvas.style.height = "100%";
	},
	
	ShowAdv: function(){
		ysdk.adv.showRewardedVideo({
			callbacks: {
				onOpen: () => {
				  console.log('Video ad open.');
				},
				onRewarded: () => {
				  SendMessage('Yandex', 'IsRewarded', 1);
				},
				onClose: () => {
				  SendMessage('Yandex', 'GetReward');
				  console.log('Video ad closed.');
				  SendMessage('Yandex', 'IsRewarded', 0);
				},
				onError: (e) => {
				  console.log('Error while open video ad:', e);
				}
			}
		})
	},
	
	ShowStartAdv: function(){
		try{
			ysdk.adv.showFullscreenAdv();
		}
		catch(e){ console.log(e) }
	},
	
	GetLanguage: function(){
		try{
			SendMessage('Yandex', 'SetLanguage', ysdk.environment.i18n.lang);
			console.log('Player language is ', ysdk.environment.i18n.lang);
		}
		catch(e) {
			console.log('Get language error: ', e);
		}
	},
	
	GetPhoto: function() {
		if(player){
			if(player.getMode() != 'lite') {
				SendMessage('Yandex', 'SetPhoto', player.getPhoto('medium'));
			}
			else{
				console.log('player.getMode() is lite');
			}
		}
		else {
			console.log('player is not defined');
		}
	},
	
	PhotoError: function() {
		SendMessage('Yandex', 'SetPhoto', 'error');
		console.log('Photo error: ', e);
	},
	
	Authentication: function() {
		try {
			if(player && ysdk){
				if(player.getMode() == 'lite') {
					ysdk.auth.openAuthDialog().then(() => {
						ysdk.getPlayer().then(_player => {
							player = _player;
							_GetPhoto();
							_GetPlayerInfo();
							_GetLanguage();
							_LoadRecord();
						});
					}).catch((e) => {
						_PhotoError();
					});
				}
			}
			else {
				_PhotoError();
			}
		}
		catch(e){
			_PhotoError();
		}
	}
	
});
