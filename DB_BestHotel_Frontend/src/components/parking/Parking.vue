<template>
  <div>
    <!-- 面包屑导航区域 -->
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>车位系统</el-breadcrumb-item>
    </el-breadcrumb>
     <!-- 卡片视图区域 -->
    <el-card>
      <el-row :gutter="20">
        <el-col :span="8">
          <el-input placeholder="请输入内容" v-model="ParkForm.parking_lot_id" clearable @clear="getParkList">
            <el-button slot="append" icon="el-icon-search" @click="getspeParkList" ></el-button>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-button type="primary" @click="addParkVisible = true">添加车位信息</el-button>
        </el-col>
      </el-row>
      <!-- 订单列表数据 -->
      <el-table :data="parklist" border stripe>
        <el-table-column type="index"></el-table-column>
        <el-table-column label="车位编号" prop="parking_lot_id"></el-table-column>
        <el-table-column label="用户编号" prop="user_id"></el-table-column>
        <el-table-column label="车牌号" prop="car_number"></el-table-column>
        <el-table-column label="操作" width="180px">
          <template slot-scope="scope">
            <el-tooltip effect="dark" content= "修改车位" placement="top" :enterable="false">
            <el-button size="mini" type="primary" icon="el-icon-edit" @click="showEditPark(scope.row.parking_lot_id)"></el-button>
            </el-tooltip>
            <el-tooltip effect="dark" content= "删除车位" placement="top" :enterable="false">
            <el-button size="mini" type="danger" icon="el-icon-delete" @click="removepark(scope.row.parking_lot_id)"></el-button>
            </el-tooltip>
          </template>
        </el-table-column>
      </el-table>
      <span class="divcss5">总条数:{{total}}</span>
       </el-card>
        <!-- 添加用户的对话框 -->
    <el-dialog title="添加车位" :visible.sync="addParkVisible" width="50%" @close="addParkClosed">
      <!-- 内容主体区域 -->
      <el-form :model="addParkForm" :rules="addParkFormRules" ref="addParkFormRef" label-width="70px">
        <el-form-item label="车位编号" prop="parking_lot_id"  size="mini" label-width="100px">
          <el-input v-model="addParkForm.parking_lot_id"></el-input>
        </el-form-item>
      </el-form>
      <!-- 底部区域 -->
      <span slot="footer" class="dialog-footer">
        <el-button @click="addParkVisible = false">取 消</el-button>
        <el-button type="primary" @click="adduserPark">确 定</el-button>
      </span>
    </el-dialog>
<!-- 修改车位的对话框 -->
    <el-dialog title="修改车位" :visible.sync="editParkVisible" width="50%" @close="editParkClosed">
       <el-form :model="editForm" :rules="editFormRules" ref="editFormRef" label-width="100px">
        <el-form-item label="用户编号" prop="user_id">
          <el-input  v-model="editForm.user_id"></el-input>
        </el-form-item>
        <el-form-item label="车牌号" prop="car_number">
          <el-input  v-model="editForm.car_number"></el-input>
        </el-form-item>
      </el-form>
       <span slot="footer" class="dialog-footer">
        <el-button @click="editParkVisible = false">取 消</el-button>
        <el-button type="primary" @click="editParkInfo">确 定</el-button>
       </span>
    </el-dialog>
  </div>
</template>
<script>

export default {
  data () {
    return {
      queryInfo: {
        // query: ''
        // pagenum: 1,
        // pagesize: 10
      },
      total: 0,
      parklist: [],
      // 控制添加订单对话框的显示与隐藏
      addParkVisible: false,
      // 修改订单
      editForm: {
      },
      // 修改车位的验证规则对象
      editFormRules: {
        userid: [
          { message: '请输入用户编号', trigger: 'blur' }
        ],
        carid: [
          { message: '请输入车牌号', trigger: 'blur' }
        ]
      },
      ParkForm: {
        parking_lot_id: ''
      },
      // 添加订单的表单数据
      addParkForm: {
        parking_lot_id: '',
        user_id: '',
        car_number: ''
      },
      delParkForm: {
        parking_lot_id: ''
      },
      // 添加表单的验证规则对象
      addParkFormRules: {
        parking_lot_id: [
          { required: true, message: '请输入车位编号', trigger: 'blur' }
        ],
        user_id: [
          { message: '请输入用户编号', trigger: 'blur' }
        ],
        car_number: [
          { message: '请输入车牌号', trigger: 'blur' }
        ]
      },
      editParkVisible: false
    }
  },
  created () {
    this.getParkList()
  },
  methods: {
    async getParkList () {
      const { data: res } = await this.$http.get('Parking', {

      })
      console.log(res)
      if (res.code !== 200) {
        return this.$message.error('获取车位列表失败！')
      }
      console.log(res)
      this.total = res.data.total
      this.parklist = res.data.msg
    },
    async getspeParkList () {
      const { data: res } = await this.$http.get('Parking/Find/' + this.ParkForm.parking_lot_id, {
      })

      if (res.code !== 200) {
        return this.$message.error('获取车位列表失败！')
      }
      console.log(res)
      this.parklist = undefined
      // eslint-disable-next-line no-array-constructor
      this.parklist = new Array()
      this.parklist[0] = res.data
      console.log(this.parklist[0])
    },
    // handleSizeChange (newSize) {
    //   this.queryInfo.pagesize = newSize
    //   this.getParkList()
    // },
    // handleCurrentChange (newPage) {
    //   this.queryInfo.pagenum = newPage
    //   this.getParkList()
    // },
    // 展示编辑对话框
    async showEditPark (id) {
      console.log(id)
      const { data: res } = await this.$http.get('Parking/Find/' + id)

      if (res.code !== 200) {
        return this.$message.error('查询车位信息失败！')
      }
      this.editForm = res.data
      this.editParkVisible = true
    },
    editParkClosed () {
      this.$refs.editFormRef.resetFields()
    },
    // 修改车位提交
    editParkInfo () {
      this.$refs.editFormRef.validate(async valid => {
        if (!valid) return
        // 发起修改订单信息的数据请求
        console.log(this.editForm)
        const { data: res } = await this.$http.post(
          'Parking/Alert/', this.editForm
        )
        console.log(123456)
        console.log(res)
        if (res.code !== 200) {
          return this.$message.error('更新车位信息失败！')
        }

        // 关闭对话框
        this.editParkVisible = false
        // 刷新数据列表
        this.getParkList()
        // 提示修改成功
        this.$message.success('更新车位信息成功！')
      })
    },
    addParkClosed () {
      this.$refs.addParkFormRef.resetFields()
    },
    adduserPark () {
      this.$refs.addParkFormRef.validate(async valid => {
        if (!valid) return
        // 可以发起添加用户的网络请求
        const { data: res } = await this.$http.post('Parking/Add', this.addParkForm)
        console.log(this.addParkForm)
        if (res.code !== 200) {
          this.$message.error('添加车位失败！')
        }

        this.$message.success('添加车位成功！')
        // 隐藏添加用户的对话框
        this.addParkVisible = false
        // 重新获取用户列表数据
        this.getParkList()
      })
    },
    async removepark (id) {
      this.delParkForm.parking_lot_id = id
      // 询问用户是否删除数据
      const confirmResult = await this.$confirm(
        '此操作将永久删除该车位, 是否继续?',
        '提示',
        {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }
      ).catch(err => err)

      // 如果用户确认删除，则返回值为字符串 confirm
      // 如果用户取消了删除，则返回值为字符串 cancel
      if (confirmResult !== 'confirm') {
        return this.$message.info('已取消删除')
      }
      const { data: res } = await this.$http.post('Parking/Del', this.delParkForm)
      if (res.code !== 200) {
        return this.$message.error('删除车位失败！')
      }

      this.$message.success('删除车位成功！')
      this.getParkList()
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
