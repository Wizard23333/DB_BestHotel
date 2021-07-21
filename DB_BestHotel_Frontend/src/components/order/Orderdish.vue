<template>
  <div>
    <!-- 面包屑导航区域 -->
    
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/welcome1' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>订单系统</el-breadcrumb-item>
      <el-breadcrumb-item>餐饮订单</el-breadcrumb-item>
    </el-breadcrumb>
     <!-- 卡片视图区域 -->
    <el-card>
      <el-row :gutter="20">
        <el-col :span="8">
          <el-input placeholder="请输入内容" v-model="queryInfo.query" clearable @clear="getOrderList">
            <el-button slot="append" icon="el-icon-search" @click="getspeParkList" ></el-button>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-button type="primary" @click="addOrderVisible = true">添加订单</el-button>
        </el-col>
      </el-row>
      <!-- 订单列表数据 -->
      <el-table :data="orderlist" border stripe>
        <el-table-column type="index"></el-table-column>
        <el-table-column label="订单编号" prop="order_id"></el-table-column>
        <el-table-column label="订单人" prop="client_name"></el-table-column>
        <el-table-column label="菜品名称" prop="dish_name"></el-table-column>
        <el-table-column label="菜品价格" prop="amount"></el-table-column>
        <el-table-column label="用户电话" prop="client_telephonenumber"></el-table-column>
        <el-table-column label="订单状态" prop="ordertype">
          <template slot-scope="scope">
            <el-tag type="success" v-if="scope.row.dish_order_state == '0'">未完成</el-tag>
            <el-tag type="danger" v-else-if="scope.row.dish_order_state == '1'">进行中</el-tag>
            <el-tag type="danger" v-else-if="scope.row.dish_order_state == '2'">已完成</el-tag>
            <el-tag type="danger" v-else>已取消</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="100px">
          <template slot-scope="scope">
            <el-tooltip effect="dark" content= "修改订单状态" placement="top" :enterable="false">
              <!-- 这里有问题 -->
            <el-button size="mini" type="primary" icon="el-icon-edit" @click="showEditOrder(scope.row.order_id)"></el-button>
            </el-tooltip>
          </template>
        </el-table-column>
      </el-table>
      <span class="divcss5">总条数:{{total}}</span>
       </el-card>
        <!-- 添加用户的对话框 -->
    <el-dialog title="添加订单" :visible.sync="addOrderVisible" width="50%" @close="addOrderClosed">
      <!-- 内容主体区域 -->
      <el-form :model="addOrderForm" :rules="addOrderFormRules" ref="addOrderFormRef" label-width="70px">
        <el-form-item label="订单人" prop="client_name" size="mini" label-width="100px">
          <el-input v-model="addOrderForm.client_name"></el-input>
        </el-form-item>
        <el-form-item label="订单人ID" prop="client_id" size="mini" label-width="100px">
          <el-input v-model="addOrderForm.client_id"></el-input>
        </el-form-item>
        <el-form-item label="菜品名称" prop="dish_name" size="mini" label-width="100px">
          <el-input v-model="addOrderForm.dish_name"></el-input>
        </el-form-item>
        <el-form-item label="菜品数量" prop="number" size="mini" label-width="100px">
          <el-input v-model="addOrderForm.number"></el-input>
        </el-form-item>
        <el-form-item label="用户电话" prop="client_telephonenumber" size="mini" label-width="100px">
          <el-input v-model="addOrderForm.client_telephonenumber"></el-input>
        </el-form-item>
        <el-form-item label="订单时间" prop="dish_order_date" size="mini" label-width="100px">
          <el-input v-model="addOrderForm.dish_order_date"></el-input>
        </el-form-item>
      </el-form>
      <!-- 底部区域 -->
      <span slot="footer" class="dialog-footer">
        <el-button @click="addOrderVisible = false">取 消</el-button>
        <el-button type="primary" @click="adduserOrder">确 定</el-button>
      </span>
    </el-dialog>
<!-- 修改订单的对话框 -->
    <el-dialog title="修改订单" :visible.sync="editOrderVisible" width="50%" @close="editOrderClosed">
       <el-form :model="editForm" :rules="editFormRules" ref="editFormRef" label-width="100px">
        <el-form-item label="订单状态" prop="dish_order_state">
          <el-input  v-model="editForm.dish_order_state"></el-input>
        </el-form-item>
      </el-form>
       <span slot="footer" class="dialog-footer">
        <el-button @click="editOrderVisible = false">取 消</el-button>
        <el-button type="primary" @click="editOrderInfo">确 定</el-button>
       </span>
    </el-dialog>
  </div>
</template>
<script>

export default {
  data () {
    return {
      queryInfo: {
        query: '',
        pagenum: 1,
        pagesize: 10
      },
      total: 0,
      orderlist: [],
      // 控制添加订单对话框的显示与隐藏
      addOrderVisible: false,
      // 修改订单
      editForm: {},
      // 修改订单的验证规则对象
      editFormRules: {
        ordertype: [
          { required: true, message: '请输入订单状态', trigger: 'blur' }
        ]
      },
      // 添加订单的表单数据
      addOrderForm: {
        client_name: '',
        client_id: '',
        client_telephonenumber: '',
        dish_name: '',
        dish_order_date: '',
        number: ''
      },
      // 添加表单的验证规则对象
      addOrderFormRules: {
        client_id: [
          { required: true, message: '请输入订单人ID', trigger: 'blur' }
        ],
        client_name: [
          { required: true, message: '请输入订单人', trigger: 'blur' }
        ],
        dish_name: [
          { required: true, message: '请输入菜品名称', trigger: 'blur' }
        ],
        client_telephonenumber: [
          { required: true, message: '请输入用户电话', trigger: 'blur' }
        ],
        anumber: [
          { required: true, message: '请输入菜品数量', trigger: 'blur' }
        ],
        dish_order_date: [
          { required: true, message: '请输入订单时间', trigger: 'blur' }
        ]
      },
      editOrderVisible: false
    }
  },
  created () {
    this.getOrderList()
  },
  methods: {
    async getOrderList () {
      console.log(8520)
      console.log(this.queryInfo)
      const { data: res } = await this.$http.post('Order/DishOrderListInfo', {
        params: this.queryInfo
      })
      if (res.code !== 200) {
        return this.$message.error('获取订单列表失败！')
      }
      console.log(res)
      this.total = res.data.total
      this.orderlist = res.data.list
    },
    async getspeParkList () {
      const { data: res } = await this.$http.post('Order/DishOrderListInfo?query=' + this.queryInfo.query, {
        params: this.queryInfo
      })
      if (res.code !== 200) {
        return this.$message.error('获取餐饮列表失败！')
      }
      this.total = res.data.total
      this.orderlist = res.data.list
    },
    handleSizeChange (newSize) {
      this.queryInfo.pagesize = newSize
      this.getOrderList()
    },
    handleCurrentChange (newPage) {
      this.queryInfo.pagenum = newPage
      this.getOrderList()
    },
    // 展示编辑对话框这里有问题
    async showEditOrder (id) {
      const { data: res } = await this.$http.post('Order/DishOrderListInfo?query=' + id, id)
      if (res.code !== 200) {
        return this.$message.error('查询订单信息失败！')
      }
      console.log(res)
      this.editForm = res.data.list[0]
      console.log(123456789987456321)
      console.log(this.editForm)
      this.editOrderVisible = true
    },
    editOrderClosed () {
      this.$refs.editFormRef.resetFields()
    },
    // 修改订单提交
    editOrderInfo () {
      this.$refs.editFormRef.validate(async valid => {
        if (!valid) return
        // 发起修改订单信息的数据请求这里有问题
        console.log(this.editForm)
        console.log(999999999)
        const { data: res } = await this.$http.post(
          '/Order/OrderDishModify?order_id=' + this.editForm.order_id + '&state=' + this.editForm.dish_order_state, this.editForm
        )
        console.log(res)
        if (res.code !== 200) {
          return this.$message.error('更新订单信息失败！')
        }

        // 关闭对话框
        this.editOrderVisible = false
        // 刷新数据列表
        this.getOrderList()
        // 提示修改成功
        this.$message.success('更新用户信息成功！')
      })
    },
    addOrderClosed () {
      this.$refs.addOrderFormRef.resetFields()
    },
    adduserOrder () {
      this.$refs.addOrderFormRef.validate(async valid => {
        if (!valid) return
        // 可以发起添加的网络请求
        console.log(123456)
        const { data: res } = await this.$http.post('Order/DishReserve?client_name=' + this.addOrderForm.client_name + '&client_id=' + this.addOrderForm.client_id + '&dish_name=' + this.addOrderForm.dish_name + '&client_telephonenumber=' + this.addOrderForm.client_telephonenumber + '&dish_order_date=' + this.addOrderForm.dish_order_date + '&number=' + this.addOrderForm.number, this.addOrderForm)
        console.log(987654)
        if (res.code !== 201) {
          this.$message.error('添加订单失败！')
        }

        this.$message.success('添加订单成功！')
        // 隐藏添加的对话框
        this.addOrderVisible = false
        // 重新获取列表数据
        this.getOrderList()
      })
    }
  }
}
</script>
<style lang="less" scoped>

.el-cascader{
  width: 100%;
}
.divcss5{
  font-size: 12px;
  color:#ff9955;
}
</style>
