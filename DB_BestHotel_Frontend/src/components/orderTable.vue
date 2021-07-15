<template>
  <div>
  
    <el-breadcrumb separator="/">
      <el-breadcrumb-item :to="{ path: 'welcome' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>房间订单信息</el-breadcrumb-item>
    </el-breadcrumb>
  <h2>订单列表</h2>
  <br>
    <el-card>
      <el-row>
        <el-col :span="7">
          <el-input
            placeholder="请输入内容"
            v-model="queryInfo.query"
            clearable
            @clear="getOrderList"
          >
            <el-button
              slot="append"
              icon="el-icon-search"
              @click="getOrderList"
            ></el-button>
          </el-input>
        </el-col>
        <el-col :span="4"></el-col>
      </el-row>
      <el-table :data="orderList" border stripe>
        <el-table-column type="index"></el-table-column>
        <el-table-column label="订单编号" prop="order_id"></el-table-column>
        <el-table-column label="客户ID" prop="client_id"></el-table-column>

        <el-table-column label="订单日期" prop="room_order_date"></el-table-column>
        <el-table-column label="交易金额" prop="amount"></el-table-column>
       
        <el-table-column label="订单状态" prop="state">
          <template slot-scope="scope">
            <el-tag type="success" v-if="scope.row.state == '0'"
              >未完成</el-tag
            >
            <el-tag type="danger" v-else-if="scope.row.state == '1'"
              >进行中</el-tag
            >
            <el-tag type="danger" v-else-if="scope.row.state == '2'"
              >已完成</el-tag
            >
            <el-tag type="danger" v-else>已取消</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="修改状态" prop="state">
          <template slot-scope="scope">
            <el-button
              type="primary"
              icon="el-icon-edit"
              @click="showEditDialog(sessionStorage.getItem('user_id'))"
              size="max"
            ></el-button>
          </template>
        </el-table-column>
        
      </el-table>

     <el-pagination
      
      layout="total"
      :total="total">
    </el-pagination>
    </el-card>
     
    <el-dialog title="取消订单" :visible.sync="editDialogVisible" width="50%">
      <el-form
        :model="editForm"
        :rules="editFormRules"
        ref="editFormRef"
        label-width="70px"
      >
      是否取消订单
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="editDialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="editstate">确 定</el-button>
      </span>
    </el-dialog>
  </div>
</template>
<script>
export default {
  data() {
    return {
      queryInfo: {
        query: "",
        pagenum: 1,
        pagesize: 10,
      },
      total: 0,
      editDialogVisible:false,
      orderList: [],
      form:{},
    };
  },
  created() {
    this.getOrderList();
  },
  methods: {
    async getOrderList() {
      // console.log(this.queryInfo.query);
       console.log(this.queryInfo.pagesize)
       console.log(333)
    
      const { data: res } = await this.$http.post(
        "Order/RoomOrderListInfo?pagesize="+this.queryInfo.pagesize+"&pagenum="+this.queryInfo.pagenum+"&query=" + this.queryInfo.query
      );
      if (res.code != 200) {
        return this.$message.error("获取失败");
      }
    console.log(this.queryInfo.pagesize)
      console.log(res);
      //console.log(111)
      //console.log(this.queryInfo.query)
      var mun=0;
      for(var i=0;i<res.data.total;i++)
      {
        if(res.data.list[i].client_id===window.sessionStorage.getItem('user_id'))
        {this.orderList.push(res.data.list[i]);mun++;}
      };
      this.total = mun;
      // this.orderList = res.data.list;
      this.queryInfo.pagesize=res.data.pagesize;
      this.queryInfo.pagenum=res.data.pagenum;
      // console.log(222)
       console.log(this.queryInfo.pagesize);
      // console.log(111)
      //console.log(this.orderList)
    },
    async showEditDialog(id) {
      const { data: res } = await this.$http.post(
        "Order/OrderQuery?order_id=" + id
      );
      if (res.code != 200) {
        return this.$message.error("请求失败");
      }
      //this.$message.success("成功了yeah")
      //console.log(token)
      this.form=res.data
      console.log(this.form.state)
      if(this.form.state==0)
      {this.editDialogVisible=true}
      else{
      return this.$message.error("无法操作");
      }
    },
    async editstate(){
      const{data:res}=await this.$http.post("Order/OrderCancel?order_id="+this.form.order_id);
      if(res.code!=200){
        return this.$message.error("请求失败");
      }
      this.editDialogVisible=false
      this.getOrderList()
      return this.$message.success("修改成功");
      //this.editDialogVisible=false
    },
    handleSizeChange(newSize) {
      console.log(newSize)
      this.queryInfo.pagesize = newSize
     // console.log(this.queryInfo.pagesize);
      this.getOrderList();
     // console.log(this.queryInfo.pagesize);
    },
    handleCurrentChange(newPage) {
      //console.log(newPage)
      this.queryInfo.pagenum = newPage;

      this.getOrderList();
      console.log(this.queryInfo.pagenum)
    },
  },
};
</script>
<style lang="less" scoped>
</style>
