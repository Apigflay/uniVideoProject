<template>
	<view>
		<video class="video" id="myVideo" :src="videoUri" controls v-if="videoUri">
			<cover-view class="video-control">
				<cover-view class="multi rate" @tap="showSwitchRate">x {{ currentRate }}</cover-view>
			</cover-view>
			<cover-view class="multi-list rate" :class="{ active: rateShow }">
				<cover-view
					v-for="(item, index) in ['0.5', '0.8', '1.0', '1.25', '1.5']"
					:key="index"
					class="multi-item rate"
					:data-rate="item"
					@tap="switchRate"
					:class="{ active: item == currentRate }"
				>
					{{ item }}
				</cover-view>
			</cover-view>
		</video>
	</view>
</template>

<script>
export default {
	name: 'SyVideo',
	data() {
		return {
			videoContext: uni.createVideoContext('myVideo'),
			rateShow: false,
			currentRate: 1.0
		};
	},
	created() {
		console.log(this.videoUri);
	},
	methods: {
		showSwitchRate(rate) {
			let that = this;
			console.log('rateShow');
			that.rateShow = true;
		},
		switchRate(e) {
			let that = this;
			let rate = Number(e.currentTarget.dataset.rate);
			that.currentRate = rate;
			that.rateShow = false;
			this.videoContext.playbackRate(rate);
		}
	},
	props: {
		videoUri: {
			type: [String],
			default: ''
		}
	}
};
</script>

<style>
.video {
	width: 100%;
}
.video-control {
	background-color: rgba(0, 0, 0, 0.1);
	height: 90rpx;
	position: absolute;
	top: 0;
	left: 0;
	width: 100%;
	z-index: 997;
}
.video-wrap {
	position: relative;
}
.multi-list.full-screen.vertical {
	height: 100vh !important;
	padding: 8vh 0;
}
.multi-list.full-screen.horizontal {
	height: 100vw !important;
	padding: 8vw 0;
}
.multi {
	position: absolute;
	right: 30rpx;
	top: 50%;
	transform: translateY(-50%);
	z-index: 998;
	width: 100rpx;
	color: #fff;
	padding: 10rpx 0;
	text-align: center;
	transition: color ease 0.3s;
}
.multi.rate {
	right: 30rpx;
}
.multi-list {
	position: absolute;
	height: 100%;
	width: 0;
	background-color: rgba(0, 0, 0, 0.65);
	top: 0;
	right: 0;
	transition: width 0.3s ease;
	z-index: 999;
	box-sizing: border-box;
	padding: 50rpx 0;
}
.multi-list.rate {
	padding: 25rpx 0;
}
.multi-list.active {
	width: 300rpx;
}
.multi-item {
	text-align: center;
	color: #fff;
	line-height: 80rpx;
	transition: color ease 0.3s;
}
.multi-item.rate {
	line-height: 70rpx;
}
.multi-item:hover,
.multi:hover {
	color: #00d8ff;
}
.multi-item.active {
	color: #00d8ff;
}
</style>
