new Vue({
    el: "#dashboard",
    data: {
        registerCount: 0,
        favoriteIntegers: [],
        recent: []
    },
    methods: {
        UpdateStats: function () {
            var vm = this;
            //three separate calls for parallelism

            //get updated count
            axios({
                method: 'get',
                url: '/ApiProxy/count'
            }).then(function (res) {
                vm.registerCount = res.data.toLocaleString();
            });

            //get updated favorites
            axios({
                method: 'get',
                url: '/ApiProxy/favorites'
            }).then(function(res){
                vm.favoriteIntegers = res.data;
            });

            //get updated most recent
            axios({
                method: 'get',
                url: '/ApiProxy/recent'
            }).then(function(res) {
                vm.recent = res.data;
            });

            //update again in 5 seconds
            setTimeout(this.UpdateStats, 5000);
        }
    },
    beforeMount: function () {
        //inital load
        this.UpdateStats();
    }
})