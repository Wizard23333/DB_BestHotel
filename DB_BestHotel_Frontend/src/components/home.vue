<template>
   <el-container class="home-container">
  <el-header>
    <h1>
      
      <div class="title">百思特酒店管理系统</div>
      <el-button type="primary" round class="but" @click="logout">退出登录</el-button></h1>
    
    </el-header>
  <el-container>
    
    <el-aside width="200px">
            <el-menu
      background-color="#2D3A4B"
      text-color="#fff"
      active-text-color="#40A0FF" router>
      <el-submenu index="welcome">
        <template slot="title">
          <i class="el-icon-document"></i>
          <span>订单系统</span>
         
        </template>
        
        <el-menu-item index="Orderroom">
            <template slot="title">
          <i class="el-icon-document"></i>
          <span>房间订单</span>
        </template>
        </el-menu-item>
        <el-menu-item index="Orderdish">
            <template slot="title">
          <i class="el-icon-document"></i>
          <span>餐饮订单</span>
        </template>
        </el-menu-item>
      </el-submenu>
      <el-submenu index="2">
        <template slot="title">
          <i class="el-icon-s-home"></i>
          <span>房间系统</span>
         
        </template>
        
        <el-menu-item index="roomList">
            <template slot="title">
          <i class="el-icon-s-home"></i>
          <span>房间列表</span>
        </template>
        </el-menu-item>
        <el-menu-item index="roomType">
            <template slot="title">
          <i class="el-icon-s-home"></i>
          <span>房间类型</span>
        </template>
        </el-menu-item>
      </el-submenu>

       <el-menu-item index="dishList">
        <template slot="title">
          <i class="el-icon-fork-spoon"></i>
          <span>餐饮系统</span>
         
        </template>
       
       
      </el-menu-item>
   
     <el-menu-item index="facility">
            <template slot="title">
          <i class="el-icon-s-platform"></i>
          <span>设施系统</span>
        </template>
        </el-menu-item>
       
     
       <el-menu-item index="parking">
            <template slot="title">
          <!-- 图标 -->
          <i class="el-icon-location"></i>
          <!-- 文本 -->
          <span>车位系统</span>
          </template>
          </el-menu-item>
           <el-menu-item index="moni">
            <template slot="title">
          <!-- 图标 -->
          <i class="el-icon-video-camera-solid"></i>
          <!-- 文本 -->
          <span>监控系统</span>
          </template>
          </el-menu-item>
           <el-menu-item index="StaffManage">
            <template slot="title">
          <!-- 图标 -->
          <i class="el-icon-s-custom"></i>
          <!-- 文本 -->
          <span>员工系统</span>
          </template>
          </el-menu-item>
          <el-menu-item index="personal">
            <template slot="title">
          <!-- 图标 -->
          <i class="el-icon-info"></i>
          <!-- 文本 -->
          <span>账户信息</span>
          </template>
          </el-menu-item>
    </el-menu>
    </el-aside>
    <el-main>
        <router-view></router-view>
    </el-main>
  </el-container>
</el-container> 
</template>
<script>
export default{
created(){
this.$router.push("/welcome1");
},
methods:{
  logout()
  {
        window.sessionStorage.clear();
        this.$router.push("/");
  }
}
}


</script>

<style lang="less" scoped>
.home-container{
    height:100%
}
.title {
  color: #ffffff;
  position: absolute;
  left: 2%;
  top: 2%;
}
.but{
  position: absolute;
  left: 91%;
  top: 1.5%;
}
.el-header{
    background-color: #2D3A4B;
}
.el-aside{
    background-color: #2D3A4B;
    .el-menu
    {
     border-right:none ;
    }
}
.el-main{
    background-color: #eaedf1;
}
</style>