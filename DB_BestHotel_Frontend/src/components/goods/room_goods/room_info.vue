<template>
  <div class="bbg_container">
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/welcome' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>我要下单</el-breadcrumb-item>
      <el-breadcrumb-item>房间信息</el-breadcrumb-item>
    </el-breadcrumb>
    <div class="bg_container"></div>
    <h2 class="title">房间列表</h2>
    
    <el-input
      v-model="input"
      placeholder="请输入内容"
      class="einput"
      clearable
      @change="search(input)"
    ></el-input>
     <el-button
      type="primary"
      icon="el-icon-search"
      class="eicon"
      @click="search(input)"
    ></el-button>
    <div class="row_container1">
      <el-row :gutter="25">
        <div v-for="(room, index) in listObj" :key="index">
          <el-col :span="5" class="ecol" v-if="room.Visible && room.VisibleP">
            <el-card
              :body-style="{ padding: '5px' }"
              class="ecard"
              v-if="room.Visible && room.VisibleP"
            >
              <img
                :src="room.pictue"
                width="100%"
                @click="room.outerVisible = true"
              />
              <div
                style="padding: 5px"
                class="bottom clearfix"
                @click="room.outerVisible = true"
              >
                <el-tag :type="room.etype" class="etag1">
                  {{ room.name }}
                </el-tag>
                <el-tag :type="room.etype" effect="dark" class="etag2">
                  {{ room.price }}¥
                  {{ room.type }}
                </el-tag>
              </div>
            </el-card>

            <el-dialog
              width="40%"
              :title="room.name"
              :visible.sync="room.outerVisible"
              append-to-body
            >
              <el-dialog
                width="30%"
                title="订单信息填写"
                :visible.sync="room.innerVisible"
                append-to-body
              >
                <span class="demonstration">您的选择是：{{ room.name }}</span>
                <br />
                <br />
                <br />
                <el-row :gutter="0">
                  <el-col :span="7"
                    ><div class="grid-content">
                      <span class="demonstration"><br />请选择入住日期：</span>
                    </div></el-col>
                  <el-col :span="10"
                    ><div class="grid-content">
                      <el-date-picker
                        v-model="inputdate"
                        type="date"
                        placeholder="选择日期"
                        value-format="yyyy-MM-dd"
                        :picker-options="pickerOptions"
                      >
                      </el-date-picker></div
                  ></el-col>
                </el-row>
                <br />
                <br />
                <el-row :gutter="2">
                  <el-col :span="7"
                    ><div class="grid-content">
                      <span class="demonstration"><br />请选择入住时长：</span>
                    </div></el-col
                  >
                  <el-col :span="10"
                    ><div class="grid-content">
                      <el-input
                        v-model="inputtime"
                        placeholder="请输入内容"
                        maxlength="3"
                        clearable
                        oninput="value=value.replace(/[^\d]/g,'')"
                      ></el-input></div
                  ></el-col>
                  <el-col :span="6"
                    ><span class="demonstration"><br />/天</span></el-col
                  >
                </el-row>         
                <br />            
                <br />
                <el-row :gutter="0">
                  <el-col :span="7"
                    ><div class="grid-content">
                      <span class="demonstration"><br />您的联系方式：</span>
                    </div></el-col
                  >
                  <el-col :span="10"
                    ><div class="grid-content">
                      <el-input
                        v-model="inputphone"
                        placeholder="请输入内容"
                        clearable
                        maxlength="11"
                        oninput="value=value.replace(/[^\d]/g,'')"
                      ></el-input></div
                  ></el-col>
                </el-row>
                <div slot="footer" class="dialog-footer">
                  <el-button @click="room.innerVisible = false"
                    >取 消</el-button
                  >
                  <el-button type="primary" @click="yuyue(room.name,index)"
                    >预约</el-button
                  >
                </div>
              </el-dialog>

              <el-carousel class="ecarousel">
                <el-carousel-item v-for="item in 4" :key="item">
                  <img :src="room.pictue" width="100%" />
                </el-carousel-item>
              </el-carousel>
              <br />
              <el-row :gutter="20">
                <el-col :span="15"
                  ><div class="grid-content bg-purple">
                    <div
                      style="
                        padding: 5px;
                        color: White;
                        font-weight: 520;
                        font-size: 17px;
                      "
                    >
                      {{ room.text }}
                    </div>
                  </div></el-col
                >
                <el-col :span="9"
                  ><div class="grid-content">
                    <el-button @click="room.outerVisible = false"
                      >取 消</el-button
                    >
                    <el-button type="primary" @click="room.innerVisible = true"
                      >订单预约</el-button
                    >
                  </div></el-col
                >
              </el-row>
            </el-dialog>
          </el-col>
        </div>
      </el-row>
    </div>

    <div class="the_button">
      <el-pagination
        :current-page.sync="currentPage"
        @current-change="handleCurrentChange"
        background
        layout="prev, pager, next"
        :total="totalPage()"
      >
      </el-pagination>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      inputdate: "",//预计入住时间ok
      inputtime: 1,//入住时长ok
      inputphone: "",//联系方式ok
      stay_name:"王小二",//入住者姓名ok
      card_code:"410711200010299019",//身份证号ok
      room_num:1,//房间数ok
      input: "",//ok
      currentPage: 1,
      pickerOptions: {
        // 限制预约时间
        disabledDate(time) {
          return time.getTime() < Date.now() - 24 * 60 * 60 * 1000;
        },
      },
      listObj: [
        // {
        //   name: "双人间",
        //   pictue: 'https://i.loli.net/2021/07/14/w29GTutakOgFN3q.jpg',
        //   text: "自然主义的规划设计, 庄重典雅的格式布局, 在现代与古典之间, 建构绵延不息的辉煌",
        //   type: "空余",
        //   etype: "success",
        //   outerVisible: false,
        //   innerVisible: false,
        //   Visible: true,
        //   VisibleP: false,
        //   price: 200,
        // },
        // {
        //   name: "单人间",
        //   pictue: require("./portfolio-2.jpg"),
        //   text: "卸下工作的一身疲惫,让身体陶醉于轻松的呵护,放飞心情,细细体会释放的雅趣这里,是一处静谧的港湾;这里,时间也放慢了脚步",
        //   type: "特价",
        //   etype: "success",
        //   outerVisible: false,
        //   innerVisible: false,
        //   Visible: true,
        //   VisibleP: false,
        //   price: 150,
        // },
        // {
        //   name: "大床房",
        //   pictue: require("./portfolio-3.jpg"),
        //   text: "精美的材质制作及亮丽的色彩搭配,华贵而不失典雅,既充分体现酒店的高品位, 又为入住者营造出温馨家园的感觉",
        //   type: "空余",
        //   etype: "success",
        //   outerVisible: false,
        //   innerVisible: false,
        //   Visible: true,
        //   VisibleP: false,
        //   price: 250,
        // },
        // {
        //   name: "三人间",
        //   pictue: require("./portfolio-4.jpg"),
        //   text: "在金黄色的阳光里,有种超越一切的高贵典雅 若未亲身体验,就会无法想象",
        //   type: "已满",
        //   etype: "danger",
        //   outerVisible: false,
        //   innerVisible: false,
        //   Visible: true,
        //   VisibleP: false,
        //   price: 300,
        // },
        // {
        //   name: "标准套间",
        //   pictue: require("./portfolio-5.jpg"),
        //   text: "城市依旧喧嚣,竞争依旧激烈,奔波的身影依旧疲惫 其实你并不孤单,只要细细品味,才纵然发现,这一路上一直有我们的真心相伴",
        //   type: "空余",
        //   etype: "success",
        //   outerVisible: false,
        //   innerVisible: false,
        //   Visible: true,
        //   VisibleP: false,
        //   price: 500,
        // },
        // {
        //   name: "豪华套间",
        //   pictue: require("./portfolio-6.jpg"),
        //   text: "就是这样,沉默而坚定着,以充满着人性化的规划,明亮 华丽的视觉传达,饱含灵性的结构布局,承载我们对新生活方式的理解",
        //   type: "空余",
        //   etype: "success",
        //   outerVisible: false,
        //   innerVisible: false,
        //   Visible: true,
        //   VisibleP: false,
        //   price: 800,
        // }
      ],
    };
  },
  created() { 
     this.getInfo();
  },
  methods: {
    async getInfo()
    { 
      var name="豪华套间";
      var pictue='';
      var text= "文案";
      var type="空余";
      var etype="success";
      var outerVisible=false;
      var innerVisible=false;
      var Visible=true;
      var VisibleP=false;
      var price=800;
      const res=await this.$http.post('Room/RoomTypeInfo');
      console.log(res.data);
      if(res.data.code!==200){console.log("请求失败")}
      else{console.log("请求成功")};
      for (let i = 0; i < res.data.data.length; i++)
      {
      name=res.data.data[i].room_type;
      pictue=res.data.data[i].room_url;
      price=res.data.data[i].room_price;
      if(res.data.data[i].room_workable==false){etype="danger",type="已满"}
      else{type="空余"+res.data.data[i].room_workable+'间'};
      text=res.data.data[i].room_explain;
      this.listObj.push({     
          name,
          pictue,
          text,
          type,
          etype,
          outerVisible,
          innerVisible,
          Visible,
          VisibleP,
          price
      }); 
      }
      this.showPage(1);
      const ress=await this.$http.get('Client');
      console.log(ress.data);
    },
    search(input) {
      for (let i = 0; i < this.listObj.length; i++) {
        var str = "";
        str = this.listObj[i].name;
        if (str.includes(input)) {
          this.listObj[i].Visible = true;
        } else {
          this.listObj[i].Visible = false;
        }
      }
      this.currentPage = 1;
      this.showPage(1);
      return;
    },
    showPage(currentPage) {
      var count = -1;
      for (let i = 0; i < this.listObj.length; i++) {
        if (this.listObj[i].Visible == true) {
          count++;
        }
        if (
          count >= (currentPage - 1) * 8  &&
          count <= currentPage * 8-1 &&
          this.listObj[i].Visible == true
        ) {
          this.listObj[i].VisibleP = true;
        } else {
          this.listObj[i].VisibleP = false;
        }
      }
    },
    handleCurrentChange(val) {
      this.showPage(val);
    },
    openMessage1() {
        this.$message({
          showClose: true,
          message: '下单成功！',
          type: 'success'
        });
      },
    openMessage2() {
        this.$message({
          showClose: true,
          message: '出错啦，下单失败',
          type: 'error'
        });
        },
    async yuyue(name,index) {
      var month =new Date().getMonth() < 9 ? "0" + (new Date().getMonth() + 1) : new Date().getMonth() + 1;
      var date = new Date().getDate() <= 9 ? "0" + new Date().getDate() : new Date().getDate();
      let id = window.sessionStorage.getItem('user_id');
      var client_id=id;//客户id
      const { data: result } = await this.$http.get('Client/'+client_id);
      var order_set_date=new Date().getFullYear() + "-" + month + "-" + date;//订单生成时间ok
      var room_type=name;//房间类型名ok
      var amount=this.room_num;//订房数量ok
      var live_name=result.client_name;//入住者姓名ok
      var card_number=this.card_code;//身份证号ok
      var order_date=this.inputdate;//预计入住时间ok
      var stay_time=this.inputtime;//入住时长ok
      var client_telephonenumber=this.inputphone;//入住者联系方式ok
      if(order_date==''||stay_time==''){this.$message({
          showClose: true,
          message: '请输入入住时间和入住时长',
          type: 'error'
        });return;}
       const { data: res } = await this.$http.post(
        'Order/RoomReserve?client_id='+client_id+'&order_date='+order_set_date+'&room_type='+room_type+'&amount='+amount+'&client_name='+live_name+'&client_identity_card_number='+card_number+'&room_order_date='+order_date+'&stay_time='+stay_time+'&client_telephonenumber='+client_telephonenumber
      );
      console.log(res);
      if(res.code!==200){console.log("请求失败"),this.openMessage2()}
      else{console.log("请求成功"),this.openMessage1()};
      this.getInfo();
      this.listObj[index].innerVisible=false;
    },
    totalPage() {
      var count = 0;
      for (let i = 0; i < this.listObj.length; i++) {
        if (this.listObj[i].Visible == true) {
          count++;
        }
      }
      return Math.ceil(count / 8) * 10;
    },
  },
};
</script>

<style scoped>
.title {
  color: #000000;
  position: absolute;
  left: 5.5%;
  top: 4%;
}
.bbg_container {
  height: 100%;
  width: 90%;
  position: relative;
  left: 0%;
  top: 0%;
}
.bg_container {
  background-color: #f4f4f5;
  height: 86%;
  width: 98%;
  border-radius: 7px;
  position: absolute;
  left: 5%;
  top: 11.5%;
}
.picture {
  padding: 3%;
}
.the_button {
  position: absolute;
  left: 50%;
  top: 90%;
  transform: translate(-50%, -50%);
}

.row_container1 {
  width: 113%;
  position: absolute;
  left: 7%;
  top: 16%;
}
.image {
  border-radius: 7px;
}
.ecol {
  padding: 0.6%;
}
.ecard {
  border-radius: 7px;
  background: #c1c1c2;
}
.einput {
  width: 15%;
  position: absolute;
  left: 82%;
  top: 3%;
}
.etag1 {
  position: relative;
  left: 8%;
}
.etag2 {
  position: relative;
  left: 26%;
}
.eicon {
  position: absolute;
  left: 98%;
  top: 3%;
}
.el-row {
  margin-bottom: 0px;
}
.el-col {
  border-radius: 4px;
}
.bg-purple-dark {
  background: #909399;
}
.bg-purple {
  background: #909399;
}
.bg-purple-light {
  background: #e5e9f2;
}
.grid-content {
  border-radius: 4px;
  min-height: 40px;
}
.row-bg {
  padding: 10px 0;
  background-color: #f9fafc;
}
.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}

.clearfix:after {
  clear: both;
}
.el-carousel__item h3 {
  color: #475669;
  font-size: 14px;
  opacity: 0.75;
  line-height: 150px;
  margin: 0;
}

.el-carousel__item:nth-child(2n) {
  background-color: #99a9bf;
}

.el-carousel__item:nth-child(2n + 1) {
  background-color: #d3dce6;
}
.block {
  width: 65%;
}
.ecarousel {
  width: 100%;
  border-radius: 6px;
}
</style>
