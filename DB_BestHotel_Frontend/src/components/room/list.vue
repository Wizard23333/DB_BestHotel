<template >

  <div class="hello">
     <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/welcome1' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>房间系统</el-breadcrumb-item>
      <el-breadcrumb-item>房间列表</el-breadcrumb-item>
    </el-breadcrumb>
    <div class="searchDiv">
     
        <!-- 查询输入框 -->
        <el-row :gutter="5">
        <el-col :span="18">
          <el-input
            v-model="input"
            placeholder="请输入房间号或房间类型"
          ></el-input>
        </el-col>
        <el-col :span="3">
          <el-button
            class="searchBtn"
            type="primary"
            icon="el-icon-search"
            @click="search(input)"
            >搜索</el-button
          >
        </el-col>
        <el-col :span="3">
          <el-button type="success" @click="toAdd">添加房间</el-button>
        </el-col>
      </el-row>


       
      <!-- 模态框 -->
      

      <el-dialog title="输入房间信息" :visible.sync="dialogFormVisible">
        <el-form>
          <el-form-item label="房间类型" :label-width="formLabelWidth">
            <el-input v-model="roomType"></el-input>
          </el-form-item>
          <el-form-item label="清理员工id" :label-width="formLabelWidth">
            <el-input v-model="room_staff_id"></el-input>
          </el-form-item>
          <el-form-item label="房间状态" :label-width="formLabelWidth">
            <el-input v-model="roomCondition"></el-input>
          </el-form-item>

          <el-form-item label="房间价格" :label-width="formLabelWidth">
            <el-input v-model="roomPrice"></el-input>
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button type="primary" @click="add(room_staff_id,roomType,roomCondition,roomPrice)">保存</el-button>
        </div>
      </el-dialog>
    </div>
    <div class="tableDiv">
    
      <el-table :data="roomlist" style="width: 100%" ref="myTable">
        <el-table-column type="index" :index="indexMethod"></el-table-column>
        <el-table-column prop="room_id" label="房间ID" width="180"></el-table-column>
        <el-table-column prop="staff_id" label="清理员工" width="180"></el-table-column>
        <el-table-column prop="room_type" label="房间类型" width="180"></el-table-column>
        <el-table-column prop="room_price" sortable label="房间价格"></el-table-column>
         <el-table-column prop="room_condition"  label="房间状态" column-key="condition"
      :filters="[ {text: '入住', value: '入住'}, {text: '空闲', value: '空闲'}, {text: '已预定', value: '已预定'}]"
      :filter-method="filterHandler">
          <template slot-scope="scope">
        <el-popover trigger="hover" placement="top">
          <p>入住人姓名: {{ scope.row.name }}</p>
          <p>入住人联系方式: {{ scope.row.phone }}</p>
          <p>入住时间: {{ scope.row.time }}</p>
          <div slot="reference" class="name-wrapper">
            <el-tag size="medium">{{ scope.row.room_condition }}</el-tag>
          </div>
        </el-popover>
      </template>
        </el-table-column>

     
        <el-table-column prop="address" label="操作">
          <template slot-scope="scope">
          <el-button type="primary" @click="edit(scope.$index,scope.row)">修改</el-button>
          <el-button type="danger" @click="dele(scope.$index,scope.row)">删除</el-button>
            <!--<i class="el-icon-close" @click="dele(scope.$index)"></i>
            <i class="el-icon-edit" @click="edit(scope.$index,scope.row)"></i>-->
          </template>
        </el-table-column>
      </el-table>
    </div>
  </div>
</template>

<script>
export default {
  name: "HelloWorld",
  data() {
    return {
      input: "",
      roomPrice: "",
      roomCondition: "",
      room_staff_id: "",
      roomType: "",
      dialogFormVisible: false,
      formLabelWidth: "100px",
      form: {},
      searchData: "",
     roomlist: [
      //  {
      //     id: "1",
      //     staff_id: "134235425",
      //     type: "单",
      //     condition: "空闲",
      //     price: "31.90"
         
      //   },
      //   {
      //    id: "2",
      //     staff_id: "134235425",
      //     type: "双",
      //     condition: "入住",
      //     price: "28.90",
      //      name: "1",
      //     phone:"1234567889",
      //     time:"1111-11-11"
      //   },
      //   {
      //    id: "3",
      //     staff_id: "134235425",
      //     type: "单",
      //     condition: "入住",
      //     price: "29.90",
      //      name: "1",
      //     phone:"1234567889",
      //     time:"1111-11-11"
      //   },
      //   {
      //    id: "4",
      //     staff_id: "134235425",
      //     type: "单",
      //     condition: "入住",
      //     price: "27.90",
      //      name: "1",
      //     phone:"1234567889",
      //     time:"1111-11-11"
      //   }
     ],
    
      oldVal: [],
      newVal: [],
      editIndex: "",
      qubie: ""
    };
  },
  created () {
    this.getroomlist()
  },
  methods: {
     formatter(row, column) {
        return row.address;
      },
    async getroomlist () {
      const { data: res } = await this.$http.post('Room/RoomInfo')
      if (res.code !== 200) {
        return this.$message.error('获取房间列表失败！')
      }
      this.roomlist = res.data;
       //  console.log(res.data);
       //  console.log(this.roomList)
     // this.total = res.data.total
    },
        
    filterHandler(value, row, column) {
        const property = column['property'];
        return row[property] === value;
      },
    handleClose(done) {
      this.$confirm("确认关闭？")
        .then(_ => {
          done();
        })
        .catch(_ => {});
    },
    // 新增和编辑书籍功能
    async add(room_staff_id,roomType, roomCondition, roomPrice) {
      // 编辑
      if (this.qubie == "edit") {
        // console.log(this.editIndex);
        var editroom = this.roomlist[this.editIndex];
        // console.log(editroom)
        editroom.room_type = roomType;
        editroom.room_condition = roomCondition;
        editroom.staff_id = room_staff_id;
        editroom.room_price = roomPrice;
        this.dialogFormVisible = false;
 var room_temp={
            room_id:editroom.room_id,
            staff_id:room_staff_id,
            room_type: roomType,
            room_condition: roomCondition,
            room_price: roomPrice
        };
       console.log(room_temp);
      const { data: res } =await this.$http.post("Room/RoomModify?room_id="+room_temp.room_id+"&room_price="+room_temp.room_price+"&room_type="+room_temp.room_type+"&room_condition="+room_temp.room_condition+"&staff_id="+room_temp.staff_id)
       if (res.code !== 200) {
        return this.$message.error('更新房间失败！')
      }
       console.log(res);
        // this.roomType = "";
      }
      // 新增
      if (this.qubie == "add") {
        var room_temp={
            room_type: roomType,
            room_condition: roomCondition,
            staff_id: room_staff_id,
            room_price: roomPrice
        };
         //console.log(room_temp);
    
       


        console.log(2);
        if (roomType == "" || roomCondition == "" || roomPrice == "") {
          this.dialogFormVisible = true;
          // console.log(1)
        } else {
          this.dialogFormVisible = false;
          // console.log(roomType, roomCondition, roomPrice);
          const { data: res } = await this.$http.post( "Room/RoomAdd?room_condition="+room_temp.room_condition+"&staff_id="+room_temp.staff_id+"&room_price="+room_temp.room_price+"&room_type="+room_temp.room_type)
         console.log(res)
         if (res.code!== 200) {
        return this.$message.error('增加房间失败！')
      }
        this.getroomlist();
        console.log(res);
        }
      }
    },
    // 删除
    async dele(index,row) {
      console.log(row.room_id);
      const { data: res } = await this.$http.post("Room/RoomDelete?room_id="+row.room_id)
       if (res.code !== 200) {
        return this.$message.error('删除房间失败！')
      }
    console.log(res)
    



      // console.log(index)
      this.roomlist.splice(index, 1);
    },
    // 点击编辑按钮
    edit(index, row) {
     



      // console.log(row.name);
      this.dialogFormVisible = true;
      this.roomType = row.room_type;
      this.roomCondition = row.room_condition;
      this.room_staff_id=row.staff_id;
      // console.log(this.room_staff_id);
      this.roomPrice = row.room_price;
      
      this.editIndex = index;
      this.qubie = "edit";
      
    },
    // 点击添加按钮
    toAdd() {
      this.dialogFormVisible = true;
      this.roomType = "";
      this.roomCondition = "空闲";
      this.roomPrice = "";
      this.qubie = "add";
    },
    // 查找
    search(input) {
     // console.log(input)
      var newData = [];
      this.roomlist.map((item, index) => {

        if (item.room_id.includes(input)||item.room_type.includes(input)) {
          
          newData.push(item);
          this.$refs.myTable.data = newData;
        }
      });
      return newData;
      // console.log(this.tableData)
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="less">

</style>
