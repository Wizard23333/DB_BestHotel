<template>
  <div>
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/welcome1' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>监控系统</el-breadcrumb-item>
    </el-breadcrumb>
    <div class="searchDiv">
      <el-row :gutter="5">
        <el-col :span="18">
          <el-input
            v-model="input"
            placeholder="请输入监控相关信息：监控编号"
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
          <el-button type="success" @click="toAdd">添加监控</el-button>
        </el-col>
      </el-row>
      <el-dialog title="监控信息" :visible.sync="dialogFormVisible" width="80%">
        <el-form
          label-position="left"
          label-width="160px"
          :rules="monitorFormRules"
          :model="monitorForm"
          ref="loginFormRef111"
        >
          <el-form-item label="监控编号：" prop="monitor_id">
            <el-input v-model="monitorForm.monitor_id"></el-input>
          </el-form-item>

          <div v-for="(item, index) in monitorForm.rooms" :key="index">
            <el-form-item
              :label="'监控管控的房间' + (index + 1) + '编号'"
              :prop="`rooms[${index}].room_id`"
              :rules="monitorFormRules.room_id"
            >
              <el-input v-model="item.room_id"></el-input>
            </el-form-item>
          </div>
          <!-- <el-form-item label="房间编号：" prop="room_id">
            <el-input
              v-model="item.room_id"
              v-for="(item, index) in monitorForm.rooms"
              :key="index"
            ></el-input>
          </el-form-item> -->

          <!-- <template slot-scope="scope">
            <div v-for="(item, index) in scope.row.rooms" :key="index">
              {{ item.room_id }}
            </div>
          </template> -->
          <el-form-item>
            <el-button
              type="primary"
              @click="add(monitorForm.monitor_id, monitorForm.rooms)"
              >保存</el-button
            >
            <el-button type="success" @click="addNew"
              >新增一个监控房间</el-button
            >
            <el-input
              v-model="deleRoomIndex"
              placeholder="删除房间编号"
              style="width: 120px"
              class="inputNumber"
            ></el-input>

            <el-button type="danger" @click="deleRoom"
              >删除一个监控房间</el-button
            >
          </el-form-item>
        </el-form>
      </el-dialog>
    </div>
    <div class="tableDiv">
      <el-table
        :data="tableData"
        style="width: 100%"
        ref="myTable"
        stripe
        fixed
      >
        <el-table-column
          prop="monitor_id"
          label="监控编号"
          align="center"
          sortable
        ></el-table-column>

        <el-table-column
          prop="rooms"
          label="监控管控的房间编号"
          align="center"
          sortable
        >
          <!-- 分行显示 -->
          <template slot-scope="scope">
            <div v-for="(item, index) in scope.row.rooms" :key="index">
              {{ item.room_id }}
            </div>
          </template>
        </el-table-column>

        <el-table-column prop="address" label="操作" width="175">
          <template slot-scope="scope">
            <!-- <i class="el-icon-close" @click="dele(scope.$index)"></i>
            <i class="el-icon-edit" @click="edit(scope.$index, scope.row)"></i> -->
            <el-button type="primary" @click="edit(scope.$index, scope.row)"
              >修改</el-button
            >
            <el-button type="danger" @click="confirmDelete(scope.$index)"
              >删除</el-button
            >
          </template>
        </el-table-column>
      </el-table>
    </div>
  </div>
</template>

<style>
el-breadcrumb-item {
  margin: 0 auto;
}
.searchDiv {
  padding: 10px;
}
.tableDiv {
  padding: 10px;
}
.inputNumber {
  margin-inline: 10px;
  /* margin-right: 10px; */
  /* padding-inline: 10px; */
}
</style>

<script>
export default {
  name: "HelloWorld",
  data() {
    return {
      temp: {
        monitor_id: "",
        rooms: [],
      },

      input: "",

      editIndex: "",

      qubie: "",

      dialogFormVisible: false,

      monitorForm: {
        monitor_id: "1234",
        rooms: [{ room_id: "0000" }],
      },

      deleRoomIndex: "",

      monitorFormRules: {
        monitor_id: [
          { required: true, message: "请输入监控编号", trigger: "blur" },
          {
            len: 4,
            message: "请输入有效的监控编号",
            trigger: "blur",
          },
        ],
        room_id: [
          { required: true, message: "请输入房间编号", trigger: "blur" },
          {
            len: 4,
            message: "请输入有效的房间编号",
            trigger: "blur",
          },
        ],
      },

      tableData: [
        // {
        //   monitor_id: "1233",
        //   rooms: [
        //     { room_id: "2222" },
        //     { room_id: "1112" },
        //     { room_id: "2244" },
        //   ],
        // },

        // {
        //   monitor_id: "1234",
        //   rooms: [
        //     { room_id: "5533" },
        //     { room_id: "5577" },
        //     { room_id: "5587" },
        //     { room_id: "5000" },
        //   ],
        // },
      ],
    };
  },
  created: function () {
    this.getData();
  },
  methods: {
    async getData() {
      // const res = await this.$http.post("try/ajax/demo_axios_post.php");
      const res = await this.$http.get("Monitor");
      // console.log(res);
      // console.log(res.data);

      if (res.status !== 200) this.$message.error("获取数据失败");
      if (res.data.code !== 200) this.$message.error("加载失败");

      this.tableData = res.data.data;

      // var temp = {
      //         monitor_id: "0006",
      //         rooms: [{ room_id: "5042" }],
      //       }

      // this.temp = {
      //   monitor_id: "0006",
      //   rooms: [{ room_id: "5042" }],
      // };
      // console.log(this.temp);
      // const { data: resw } = await this.$http.post("Monitor/Add", this.temp);
      // console.log(90009);
      // console.log(resw);
      // console.log(resw.data);

      // this.$axios
      //   .post("Monitor/Add", {
      //     monitor_id: "0006",
      //     rooms: [{ room_id: "5042" }],
      //   })
      //   .then((res) => {
      //     console.log("res=======",res)
      //     //***省略****
      //   })
      //   .catch((err) => console.log(err));

      //TODO
    },
    //点击添加按钮
    toAdd() {
      this.dialogFormVisible = true;
      this.deleRoomIndex = "";

      this.$nextTick(() => {
        this.$refs["loginFormRef111"].clearValidate(); //重置表单状态
      });

      var tempMonitor = {
        monitor_id: "",
        rooms: [{ room_id: "" }],
      };

      // console.log(tempMonitor);

      this.monitorForm = tempMonitor;

      // console.log(this.monitorForm);

      this.qubie = "add";
    },
    // 点击编辑按钮
    edit(index, row) {
      this.$nextTick(() => {
        this.$refs["loginFormRef111"].clearValidate(); //重置表单状态
      });
      this.dialogFormVisible = true;
      this.deleRoomIndex = "";

      // console.log(row);//

      var tempMonitor = {
        monitor_id: row.monitor_id,
        rooms: row.rooms,
      };
      //console.log(tempMonitor);

      this.monitorForm = tempMonitor;

      this.editIndex = index;
      this.qubie = "edit";
    },
    //点击保存按钮
    add(monitor_id, rooms) {
      this.$refs.loginFormRef111.validate(async (validTag) => {
        if (!validTag) {
          return;
        }
        if (this.qubie == "edit") {
          var editBook = this.tableData[this.editIndex];

          // editBook.monitor_id = monitor_id;
          // editBook.rooms = rooms;

          var postMonitor = {
            monitor_id: editBook.monitor_id,
            rooms: editBook.rooms,
          };

          console.log(postMonitor);
          const res1 = await this.$http.post("Monitor/Del", postMonitor);
          if (res1.status !== 200) return this.$message.error("操作失败1");
          const res2 = await this.$http.post("Monitor/Add", postMonitor);
          if (res2.status !== 200) return this.$message.error("操作失败2");
          if (res2.data.code !== 200) {
            this.$message.error("修改失败");
          } else {
            this.$message.success("修改成功");
          } 

          this.getData();

          this.dialogFormVisible = false;
        }
        if (this.qubie == "add") {
          var tempMonitor = {
            monitor_id: monitor_id,
            rooms: rooms,
          };
          // console.log(tempMonitor);
          const res = await this.$http.post("Monitor/Add", tempMonitor);
          // console.log(res);

          if (res.status !== 200) return this.$message.error("获取数据失败");
          if (res.data.code !== 200) {
            this.$message.error("添加失败");
          } else {
            this.$message.success("添加成功");
          }
          // this.tableData.push(tempMonitor);
          this.getData();

          this.dialogFormVisible = false;
        }
      });
    },
    //点击添加监控房间按钮
    addNew() {
      this.monitorForm.rooms.push({ room_id: "" });
      console.log(tempMonitor);
    },
    //删除一个房间
    async deleRoom() {
      this.monitorForm.rooms.splice(this.deleRoomIndex - 1, 1);
    },
    confirmDelete(index) {
      this.$confirm("此操作将永久删除该监控, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          this.dele(index);
          this.$message({
            type: "success",
            // message: "删除成功!",
          });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除",
          });
        });
    },
    // 删除
    async dele(index) {
      console.log(index);

      var tempMonitor = this.tableData[index];

      console.log(tempMonitor);

      const res = await this.$http.post("Monitor/Del", tempMonitor);

      console.log(res);
      if (res.status !== 200) return this.$message.error("获取数据失败");
          if (res.data.code !== 200) {
            this.$message.error("删除失败");
          } else {
            this.$message.success("删除成功");
          }

      this.tableData.splice(index, 1);
    },

    search(input) {
      var newData = [];
      this.tableData.map((item, index) => {
        if (item.monitor_id.includes(input) || item.rooms.includes(input)) {
          newData.push(item);
          this.$refs.myTable.data = newData;
        }
      });
      return newData;
    },
  },
};
</script>