mergeInto(LibraryManager.library, {

    RateGame: function () {
        YaGames.init().then(ysdk => ysdk.feedback.canReview()
        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.log(feedbackSent);
                    })
            } else {
                console.log(reason)
            }
        })
        );

    },

    SaveExtern: function (date) {
        var dateString = UTF8ToString(date);
        var myobj = JSON.parse(dateString)
        YaGames.init().then(ysdk => player.setData(myobj));
    },

    Ready: function () {
        YaGames.init()
        .then((ysdk) => {
            // Сообщаем платформе, что игра загрузилась и можно начинать играть.
            ysdk.features.LoadingAPI.ready()
        })
        .catch(console.error);
    },

    LoadExtern: function () {
        YaGames.init().then(ysdk => player.getData().then(_date => {
            const myJSON = JSON.stringify(_date);
            myGameInstance.SendMessage('Yandex', 'LoadData', myJSON);
            ysdk.features.GameplayAPI.start();
        }));
    },

    WatchAd: function () {
        YaGames.init().then(ysdk => ysdk.adv.showFullscreenAdv({
        callbacks: {
            onOpen: () => {
                ysdk.features.GameplayAPI.stop();
            },
            onClose: function(wasShown) {
                myGameInstance.SendMessage('Yandex', 'EndOfAd');
                ysdk.features.GameplayAPI.start();
            },
            onError: function(error) {
                myGameInstance.SendMessage('Yandex', 'EndOfAd');
                ysdk.features.GameplayAPI.start();
            },
        }
    }));
    },

    WatchReward: function () {
        YaGames.init().then(ysdk => ysdk.adv.showRewardedVideo({
        callbacks: {
            onOpen: () => {
                console.log('Video ad open.');
                ysdk.features.GameplayAPI.stop();
            },
            onRewarded: () => {
                myGameInstance.SendMessage('Yandex', 'Reward');
                ysdk.features.GameplayAPI.start();
            },
            onClose: () => {
                myGameInstance.SendMessage('Yandex', 'NotReward');
                ysdk.features.GameplayAPI.start();
            },
            onError: (e) => {
                myGameInstance.SendMessage('Yandex', 'NotReward');
                ysdk.features.GameplayAPI.start();
            }
        },
        }));
    },

    GetLang: function () {
            var lang = ysdk.environment.i18n.lang;
            myGameInstance.SendMessage('Yandex', 'SetLang', lang);
    },
});