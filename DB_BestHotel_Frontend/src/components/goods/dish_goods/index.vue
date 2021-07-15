<template>
  <div class="bbg_container">
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/welcome' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>我要下单</el-breadcrumb-item>
      <el-breadcrumb-item>菜单信息</el-breadcrumb-item>
    </el-breadcrumb>
    <div class="bg_container"></div>
    <h2 class="title">菜品列表</h2>

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
                <br /><br />
                <el-row :gutter="2">
                  <el-col :span="7"
                    ><div class="grid-content">
                      <span class="demonstration"><br />请选择点餐份数：</span>
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
                    ><span class="demonstration"><br />/份</span></el-col
                  >
                </el-row>
                <br />
                <div slot="footer" class="dialog-footer">
                  <el-button @click="room.innerVisible = false"
                    >取 消</el-button
                  >
                  <el-button type="primary" @click="yuyue(room.name,index)"
                    >点餐</el-button
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
                      >下单点餐</el-button
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
      inputtime: 1,
      input: "",
      currentPage: 1,
      pickerOptions: {
        // 限制预约时间
        disabledDate(time) {
          return time.getTime() < Date.now() - 24 * 60 * 60 * 1000;
        },
      },
      listObj: [
        // {
        //   name: "煲仔饭",
        //   pictue: require("./portfolio-1.png"),
        //   text: "煲仔饭是广东地区特色传统名点。是以大米、广式腊肠、青菜、鸡蛋、色拉油、老抽、鲜味汁、白糖、香油等为原材料制作而成。",
        //   type: "空余",
        //   etype: "success",
        //   outerVisible: false,
        //   innerVisible: false,
        //   Visible: true,
        //   VisibleP: false,
        //   price: 200,
        // },
        // {
        //   name: "鲍鱼煲",
        //   pictue: require("./portfolio-2.png"),
        //   text: "鲍鱼煲鸡是一道色香味俱全的名肴，属于粤菜系。是道经典的国宴粤菜，取材严格，工艺复杂，讲究鲍鱼够出味，鸡肉够鲜嫩，色香味俱全，回味无穷。",
        //   type: "特价",
        //   etype: "success",
        //   outerVisible: false,
        //   innerVisible: false,
        //   Visible: true,
        //   VisibleP: false,
        //   price: 150,
        // },
        // {
        //   name: "粉丝扇贝",
        //   pictue: require("./portfolio-3.png"),
        //   text: "蒜蓉粉丝蒸扇贝是广东省经典的菜肴，其用主料是蒜蓉、粉丝以及扇贝，属于粤式海鲜蒸菜。主要烹饪工艺是蒸，工序较为复杂，用时需1小时左右、十分费时。",
        //   type: "空余",
        //   etype: "success",
        //   outerVisible: false,
        //   innerVisible: false,
        //   Visible: true,
        //   VisibleP: false,
        //   price: 250,
        // },
        // {
        //   name: "卤鸭头",
        //   pictue: require("./portfolio-4.png"),
        //   text: "卤鸭头是是一道简单的小吃，主料是鸭头、蒜仁、辣椒、青葱等。",
        //   type: "已满",
        //   etype: "success",
        //   outerVisible: false,
        //   innerVisible: false,
        //   Visible: true,
        //   VisibleP: false,
        //   price: 300,
        // },
        // {
        //   name: "麻辣小龙虾",
        //   pictue: require("./portfolio-5.png"),
        //   text: "麻辣小龙虾又名口味虾、长沙口味虾、香辣小龙虾，是湖南著名的地方小吃。麻辣小龙虾以小龙虾为主材，配以辣椒、花椒和其他香辛料制成。成菜后，色泽红亮，口味辣并鲜香。",
        //   type: "空余",
        //   etype: "success",
        //   outerVisible: false,
        //   innerVisible: false,
        //   Visible: true,
        //   VisibleP: false,
        //   price: 500,
        // },
        // {
        //   name: "麻婆豆腐",
        //   pictue: require("./portfolio-6.png"),
        //   text: "是四川省传统名菜之一，属于川菜，主料为：豆腐，辅料为：蒜苗、牛肉沫，调料为：豆瓣、辣椒面和花椒面、酱油、生抽、老抽、糖、淀粉等",
        //   type: "空余",
        //   etype: "success",
        //   outerVisible: false,
        //   innerVisible: false,
        //   Visible: true,
        //   VisibleP: false,
        //   price: 800,
        // },
        // {
        //   name: "冒菜",
        //   pictue: require("./portfolio-7.png"),
        //   text: "冒菜是一道以肉类、豆制品、青菜、海鲜、菌菇类作为主要食材制作而成的菜品，起源于成都，具有四川特色的传统小吃。",
        //   type: "空余",
        //   etype: "success",
        //   outerVisible: false,
        //   innerVisible: false,
        //   Visible: true,
        //   VisibleP: false,
        //   price: 800,
        // },
        // {
        //   name: "梅菜扣肉",
        //   pictue: require("./portfolio-8.png"),
        //   text: "汉族传统名菜，属客家菜。制作材料有五花肉、梅菜、葱白、姜片等。",
        //   type: "空余",
        //   etype: "success",
        //   outerVisible: false,
        //   innerVisible: false,
        //   Visible: true,
        //   VisibleP: false,
        //   price: 800,
        // },
        // {
        //   name: "牛排",
        //   pictue: require("./portfolio-9.png"),
        //   text: "西餐中常见的食物之一。牛排的烹调方法以煎和烤制为主。",
        //   type: "空余",
        //   etype: "success",
        //   outerVisible: false,
        //   innerVisible: false,
        //   Visible: true,
        //   VisibleP: false,
        //   price: 800,
        // },
        // {
        //   name: "清蒸海虾",
        //   pictue: require("./portfolio-10.png"),
        //   text: "大虾含有丰富的优质蛋白质、维生素A、维生素B1、维生素B2、尼克酸及多种矿物质，营养丰富，能补肾健胃，有利胚胎器官的形成。",
        //   type: "空余",
        //   etype: "success",
        //   outerVisible: false,
        //   innerVisible: false,
        //   Visible: true,
        //   VisibleP: false,
        //   price: 800,
        // },
        // {
        //   name: "寿司",
        //   pictue: require("./portfolio-11.png"),
        //   text: "日本传统美食，日本古代时候的寿司，是用盐和米腌制的咸鱼，后经演变形成如今的寿司。",
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
      var name="麻婆豆腐";
      var pictue= '';
      var text= "文案";
      var type="空余";
      var etype="success";
      var outerVisible=false;
      var innerVisible=false;
      var Visible=true;
      var VisibleP=false;
      var price=800;
      const res=await this.$http.post("Dish/ReturnAll_Dish");
      if(res.data.code!==200){console.log("请求失败")}
      else{console.log("请求成功")};
      for (let i = 0; i < res.data.data.length; i++)
      {
      name=res.data.data[i].dish_name;
      pictue=res.data.data[i].dish_picture;
      price=res.data.data[i].dish_price;
      text=res.data.data[i].dish_explain;
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
    let id = window.sessionStorage.getItem('user_id');
    var client_id=id;
    const { data: result } = await this.$http.get('Client/'+client_id);
    // console.log(result);
    var client_name=result.client_name; 
    var client_telephonenumber=result.client_mobile;
    var dish_name=name;
    var number=this.inputtime;
    var month =new Date().getMonth() < 9 ? "0" + (new Date().getMonth() + 1) : new Date().getMonth() + 1;
    var date = new Date().getDate() <= 9 ? "0" + new Date().getDate() : new Date().getDate();
    var order_set_date=new Date().getFullYear() + "-" + month + "-" + date;//订单生成时间ok
      if(number==''){this.$message({
          showClose: true,
          message: '请输入点餐份数',
          type: 'error'
        });return;}
       const { data: res } = await this.$http.post('Order/DishReserve?client_name='+client_name+'&client_id='+client_id+'&dish_name='+dish_name+'&client_telephonenumber='+client_telephonenumber+'&dish_order_date='+order_set_date+'&number='+number);
      // console.log(res);
      if(res.code!==200){console.log("请求失败"),this.openMessage2()}
      else{console.log("请求成功"),this.openMessage1()};
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
  left: 30%;
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
