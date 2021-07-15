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

        <el-menu-item index="welcome">
            <template slot="title">
          <i class="el-icon-s-home"></i>
          <span>欢迎</span>
        </template>
        </el-menu-item>
        <el-menu-item index="information">
            <template slot="title">
          <i class="el-icon-user-solid"></i>
          <span>我的信息</span>
        </template>
        </el-menu-item>
      <el-submenu index="2">
        <template slot="title">
          <i class="el-icon-s-order"></i>
          <span>我的订单</span>
         
        </template>
        
        <el-menu-item index="orderTable">
            <template slot="title">
          <i class="el-icon-s-home"></i>
          <span>房间订单</span>
        </template>
        </el-menu-item>
        <el-menu-item index="dishTable">
            <template slot="title">
          <i class="el-icon-fork-spoon"></i>
          <span>餐饮订单</span>
        </template>
        </el-menu-item>
      </el-submenu>

       <el-submenu index="3">
        <template slot="title">
          <i class="el-icon-s-goods"></i>
          <span>我要下单</span>
         
        </template>
        <el-menu-item index="goodsRoom">
            <template slot="title">
          <i class="el-icon-s-home"></i>
          <span>房间信息</span>
        </template>
        </el-menu-item>
        
      
    <el-menu-item index="goodsDish">
            <template slot="title">
          <i class="el-icon-fork-spoon"></i>
          <span>菜单信息</span>
        </template>
        </el-menu-item>
        </el-submenu>
            
   
      
        
      
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
this.$router.push("/welcome");
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
    height: 100%;
    .el-menu
    {
     border-right:none ;
    }

}
.el-main{
    background-color: #eaedf1;

}
</style>