<template>
  <div id="centerRight1">
    <div class="bg-color-black">
      <div class="d-flex pt-2 pl-2">
        <span>
          <icon name="chart-line" class="text-icon"></icon>
        </span>
        <div class="d-flex">
          <span class="fs-xl text mx-2">人员通行记录</span>
        </div>
      </div>
      <div class="d-flex jc-center body-box">
        <dv-scroll-board class="dv-scr-board" :config="config" ref="scrollBoard"/>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      config: {
        header: ['地点名', '时间'],
        data: [
      
        ],
        rowNum: 3, //表格行数
        headerHeight: 35,
        headerBGC: '#0f1325', //表头
        oddRowBGC: '#0f1325', //奇数行
        evenRowBGC: '#171c33', //偶数行
        index: true,
        columnWidth: [50],
        indexHeader:'#',
        align: ['center'],
      },
      socketPath: "ws://127.0.0.1:18001",
      socket: null,
      personData:[
       
         ]
    }
  },
  methods:{
    handelSend() {
      if (!this.msg.trim().length) {
        return;
      }
      this.socket.send(
        JSON.stringify({
          user: this.username,
          msg: this.msg,
          dateTime: new Date().getTime(),
        })
      );
      this.msg = "";
    },
    init() {
      let _this = this;
      if (typeof WebSocket === "undefined") {
        alert("您的浏览器不支持socket");
      } else {
        // 实例化socket
        _this.socket = new WebSocket(_this.socketPath);
        _this.socket.onopen = _this.open;
        _this.socket.onerror = _this.error;
        _this.socket.onclose = _this.close;
        _this.socket.onmessage = _this.message;
      }
    },
    open(e) {
      console.log("FE: WebSocket open", e);
    },
    error(e) {
      console.log("FE: WebSocket error", e);
    },
    close(e) {
      console.log("FE: WebSocket close", e);
    },
    message(e) {
      debugger
      var data=JSON.parse(e.data);
      console.log(JSON.parse(e.data));
      debugger
    
      this.personData.push([data.data.deviceName, data.data.alarmTime])
      this.config = {
        header: ['地点名', '时间'],
        data: this.personData,
        rowNum: 3, //表格行数
        headerHeight: 35,
        headerBGC: '#0f1325', //表头
        oddRowBGC: '#0f1325', //奇数行
        evenRowBGC: '#171c33', //偶数行
        index: true,
        columnWidth: [50],
        indexHeader:'#',
        waitTime:1000,
        align: ['center'],
        carousel:"page"
      }
    },
  },
  created() {

    this.init();
    this.config = {   
       header: ['地点名', '时间'],
        data:this.personData,
        rowNum: 3, //表格行数
        headerHeight: 35,
        headerBGC: '#0f1325', //表头
        oddRowBGC: '#0f1325', //奇数行
        evenRowBGC: '#171c33', //偶数行
        index: true,
        columnWidth: [50],
        indexHeader:'#',
        waitTime:500,
        align: ['center'],
        carousel:"page"
      } // 这种双向绑定是有效的   

     
  }
}
</script>

<style lang="scss" scoped>
$box-height: 410px;
$box-width: 300px;
#centerRight1 {
  padding: 16px;
  padding-top: 20px;
  height: $box-height;
  width: $box-width;
  border-radius: 5px;
  .bg-color-black {
    height: $box-height - 30px;
    border-radius: 10px;
  }
  .text {
    color: #c3cbde;
  }
  .body-box {
    border-radius: 10px;
    overflow: hidden;
    .dv-scr-board {
      width: 270px;
      height: 340px;
    }
  }
}
</style>
