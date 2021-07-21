<template>
  <div>
    <!-- 面包屑导航区域 -->
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/welcome1' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>订单系统</el-breadcrumb-item>
      <el-breadcrumb-item>房间订单</el-breadcrumb-item>
    </el-breadcrumb>
     <!-- 卡片视图区域 -->
    <el-card>
      <el-row :gutter="20">
        <el-col :span="8">
          <el-input placeholder="请输入内容" v-model="queryInfo.query" clearable @clear="getOrderList">
            <el-button slot="append" icon="el-icon-search" @click="getOrderList" ></el-button>
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
        <el-table-column label="身份证号" prop="client_identity_card_number"></el-table-column>
        <el-table-column label="入住房间" prop="room_id"></el-table-column>
        <el-table-column label="入住时间" prop="room_order_date"></el-table-column>
        <el-table-column label="入住时长/天" prop="stay_time"></el-table-column>
        <el-table-column label="房间类型" prop="room_type"></el-table-column>
        <el-table-column label="房间价格" prop="amount"></el-table-column>
        <el-table-column label="用户电话" prop="client_telephonenumber"></el-table-column>
        <el-table-column label="订单状态" prop="state">
          <template slot-scope="scope">
            <el-tag type="success" v-if="scope.row.state == '0'">未完成</el-tag>
            <el-tag type="danger" v-else-if="scope.row.state == '1'">进行中</el-tag>
            <el-tag type="danger" v-else-if="scope.row.state == '2'">已完成</el-tag>
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
        <el-form-item label="订单时间" prop="order_date" size="mini" label-width="100px">
          <el-input v-model="addOrderForm.order_date"></el-input>
        </el-form-item>
        <el-form-item label="身份证号" prop="client_identity_card_number" size="mini" label-width="100px">
          <el-input v-model="addOrderForm.client_identity_card_number"></el-input>
        </el-form-item>
        <el-form-item label="入住时间" prop="room_order_date" size="mini" label-width="100px">
          <el-input v-model="addOrderForm.room_order_date"></el-input>
        </el-form-item>
        <el-form-item label="入住时长" prop="stay_time" size="mini" label-width="100px">
          <el-input v-model="addOrderForm.stay_time"></el-input>
        </el-form-item>
        <el-form-item label="房间类型" prop="room_type" size="mini" label-width="100px">
          <el-input v-model="addOrderForm.room_type"></el-input>
        </el-form-item>
        <el-form-item label="房间价格" prop="amount" size="mini" label-width="100px">
          <el-input v-model="addOrderForm.amount"></el-input>
        </el-form-item>
        <el-form-item label="用户电话" prop="client_telephonenumber" size="mini" label-width="100px">
          <el-input v-model="addOrderForm.client_telephonenumber"></el-input>
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
        <el-form-item label="订单状态" prop="state">
          <el-input  v-model="editForm.state"></el-input>
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
        state: [
          { required: true, message: '请输入订单状态', trigger: 'blur' }
        ]
      },
      OrderForm: {
        order_id: ''
      },
      // 添加订单的表单数据
      addOrderForm: {
        client_id: '',
        client_name: '',
        client_identity_card_number: '',
        room_order_date: '',
        stay_time: '',
        room_type: '',
        client_telephonenumber: '',
        order_date: '',
        amount: ''
      },
      // 添加表单的验证规则对象
      addOrderFormRules: {
        client_id: [
          { required: true, message: '请输入订单人ID', trigger: 'blur' }
        ],
        order_date: [
          { required: true, message: '请输入订单时间', trigger: 'blur' }
        ],
        client_name: [
          { required: true, message: '请输入订单人', trigger: 'blur' }
        ],
        client_identity_card_number: [
          { required: true, message: '请输入身份证号', trigger: 'blur' }
        ],
        room_order_date: [
          { required: true, message: '请输入入住时间', trigger: 'blur' }
        ],
        stay_time: [
          { required: true, message: '请输入入住时长', trigger: 'blur' }
        ],
        room_type: [
          { required: true, message: '请输入房间类型', trigger: 'blur' }
        ],
        amount: [
          { required: true, message: '请输入房间价格', trigger: 'blur' }
        ],
        client_telephonenumber: [
          { required: true, message: '请输入用户电话', trigger: 'blur' }
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
      const { data: res } = await this.$http.post('Order/RoomOrderListInfo?pagesize=' + this.queryInfo.pagesize + '&pagenum=' + this.queryInfo.pagenum + '&query=' + this.queryInfo.query, {
        params: this.queryInfo
      })
      if (res.code !== 200) {
        return this.$message.error('获取订单列表失败！')
      }

      this.total = res.data.total
      this.orderlist = res.data.list
      console.log(this.orderlist)
    },
    async getspeParkList () {
      const { data: res } = await this.$http.post('Order/OrderQuery?order_id=' + this.OrderForm.order_id, {
        params: this.OrderForm.order_id
      })
      if (res.code !== 200) {
        return this.$message.error('获取车位列表失败！')
      }
      console.log(res)
      console.log(987654)
      this.orderlist = undefined
      // eslint-disable-next-line no-array-constructor
      this.orderlist = new Array()
      this.orderlist[0] = res.data
      console.log(this.parklist[0])
    },
    handleSizeChange (newSize) {
      this.queryInfo.pagesize = newSize
      this.getOrderList()
    },
    handleCurrentChange (newPage) {
      this.queryInfo.pagenum = newPage
      this.getOrderList()
    },
    // 展示编辑对话框
    async showEditOrder (id) {
      console.log(id)
      const { data: res } = await this.$http.post('Order/OrderQuery?order_id=' + id, id)
      console.log(res)
      console.log(987465132)
      if (res.code !== 200) {
        return this.$message.error('查询订单信息失败！')
      }

      // this.editForm.order_id = res.data.order_id
      // this.editForm.state = res.data.state
      this.editForm = res.data
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
        console.log(123456789)
        const { data: res } = await this.$http.post(
          '/Order/OrderModify?order_id=' + this.editForm.order_id + '&state=' + this.editForm.state, this.editForm
        )
        console.log(1919191919191)
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
    addressDialogClosed () {
      this.$refs.addressFormRef.resetFields()
    },
    addOrderClosed () {
      this.$refs.addOrderFormRef.resetFields()
    },
    adduserOrder () {
      this.$refs.addOrderFormRef.validate(async valid => {
        if (!valid) return
        console.log(12312313213213213213)
        console.log(this.addOrderForm)
        // 可以发起添加用户的网络请求、、、、、、、、、、这里有问题    Order/RoomReserve?client_id=' + this.addOrderForm.client_name + '&order_date=' + this.addOrderForm.room_order_date + '&room_type=' + this.addOrderForm.room_type + '&stay_time=' + this.addOrderForm.stay_time + '&client_telephonenumber=' + this.addOrderForm.client_identity_card_number
        const { data: res } = await this.$http.post('/Order/RoomReserve?client_id=' + this.addOrderForm.client_id + '&order_date=' + this.addOrderForm.order_date + '&room_type=' + this.addOrderForm.room_type + '&amount=' + this.addOrderForm.amount + '&client_name=' + this.addOrderForm.client_name + '&client_identity_card_number=' + this.addOrderForm.client_identity_card_number + '&room_order_date=' + this.addOrderForm.room_order_date + '&stay_time=' + this.addOrderForm.stay_time + '&client_telephonenumber=' + this.addOrderForm.client_telephonenumber,
          this.addOrderForm)
        console.log(res)
        console.log(12312313213213213213)
        if (res.code !== 200) {
          this.$message.error('添加订单失败！')
        }

        this.$message.success('添加订单成功！')
        // 隐藏添加用户的对话框
        this.addOrderVisible = false
        // 重新获取用户列表数据
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
