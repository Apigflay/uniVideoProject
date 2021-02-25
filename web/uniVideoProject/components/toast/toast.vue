<template>
    <view class="loading_box" v-show="is_alltoast" @click="switch_loading">
        <view class="loading">
			11111111
			222222222
			3333333
            <view class="loader loader-17">
              <view class="css-square square1"></view>
              <view class="css-square square2"></view>
              <view class="css-square square3"></view>
              <view class="css-square square4"></view>
              <view class="css-square square5"></view>
              <view class="css-square square6"></view>
              <view class="css-square square7"></view>
              <view class="css-square square8"></view>
            </view>
             <!-- <view class="loader loader-4"></view> -->
        </view>
    </view>
</template>

<script>
    export default {
        data() {
            return {
                
            };
        },
        methods:{
            switch_loading(){
                this.$store.commit("set_alltoast")
            }
        },
        //实测直接在标签属性里写  $store.state.XX  拿不到数据  所以这里通过 计算属性去监听一下
        computed:{
            is_alltoast(){
                return this.$store.state.alltoast
				// this.$loading();
				// this.$loading(false);
            }
        }
    }
</script>
