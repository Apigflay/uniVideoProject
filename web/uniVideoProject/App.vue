<script>
	export default {
		onLaunch: function() {
			console.log('App Launch')
			var that = this;
			uni.getStorage({
				key: 'storage_state',
				success: function (res) {
					var vuexData = JSON.parse(res.data);
					Object.keys(vuexData).map(item => {
						that.$store.state[item] = vuexData[item]
					});
				}
			});
		},
		onShow: function() {
			console.log('App Show')
		},
		onHide: function() {
			console.log('App Hide')
			uni.setStorage({
				key: 'storage_state',
				data: JSON.stringify(this.$store.state),
				success: function () {
				}
			});
			
		}
		
	}
</script>

<style>
	/*每个页面公共css */
</style>
