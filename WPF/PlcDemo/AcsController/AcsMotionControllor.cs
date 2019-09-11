using ACS.SPiiPlusNET;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace AcsController
{
    public class AcsMotionControllor
    {
        public enum AxisIndex  //用于生成错误处理信息
        {
            ACSC_NONE = -1,

            AXIS_INDEX_X = 1,
            AXIS_INDEX_Y = 0,
            AXIS_INDEX_Z = 6,
        };

        public bool[] IsHome = Enumerable.Repeat(false, Enum.GetValues(typeof(AxisIndex)).Length).ToArray();
        protected readonly static int TIMEOUT = 120000;
        protected Api _acs_api = null;
        protected int _total_axis = 0;
        protected bool _isConnect = false;


        private static AcsMotionControllor _instance = null;
        private AcsMotionControllor()
        {
            _acs_api = new Api();
        }
        public static AcsMotionControllor Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AcsMotionControllor();
                return _instance;
            }
        }

        public void ConnectIP(string ip)
        {
            try
            {
                _acs_api.OpenCommEthernet(ip, (int)EthernetCommOption.ACSC_SOCKET_STREAM_PORT); //UDP
                _isConnect = true;
            }
            catch (Exception ex)
            {
                throw new AcsConnectException("Connect to ACSController", "错误信息：无法连接ACS控制器！" + ex.Message);
            }
        }
        public void ConnectSimulator()
        {
            try
            {
                _acs_api.OpenCommSimulator();
            }
            catch (Exception ex)
            {
                throw new AcsConnectException("Connect to ACSSimulator", "错误信息：无法连接ACS仿真程序！" + ex.Message);
            }
        }
        public void Disconnect()
        {
            try
            {
                Thread.Sleep(500);
                _acs_api.CloseComm();
            }
            catch (Exception ex)
            {
                throw new AcsConnectException("Disconnect to ACSController", "错误信息：无法与ACS断开连接！" + ex.Message);
            }
        }
        public void Reboot()
        {
            // 断掉控制器的电以后或拓扑结构改变后，上电，通讯之后Reboot，再这个过程中通讯会断开，Reboot完成，并且需要软件延时自动再次完成连接
            try
            {
                if (this.ReadInt("Rebooted") == 0)
                {
                    _acs_api.ControllerReboot(120000);  // 2 minus.
                    this.WriteVariable("Rebooted", 1);
                    if (!this.IsConnect)
                    {
                        MessageBox.Show("Reboot失败，请检查！");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new AcsConnectException("Reboot", "错误信息：自动重新连接失败！" + ex.Message);
            }
        }
        public double GetSysinfo(int key)
        {
            try
            {
                return _acs_api.SysInfo(key);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("GetSysinfo", "错误信息：获取系统信息失败！" + ex.Message);
            }
        }
        public bool IsConnect
        {
            get
            {
                try
                {
                    this._total_axis = (int)GetSysinfo(13);
                    return _total_axis > 0;
                }
                catch (Exception ex)
                {
                    throw new AcsReadWriteException("IsConnect", "错误信息：检查连接状态失败！" + ex.Message);
                }
            }
        }

        // motor part
        public uint GetAxisState(int axis)
        {
            try
            {
                return (uint)(_acs_api.GetAxisState((ACS.SPiiPlusNET.Axis)axis));
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("GetAxisState", "错误信息：获取轴状态失败！" + ex.Message);
            }
        }
        public bool IsMotorMoving(int axis)
        {
            try
            {
                int count = 200;
                bool value = (ReadIntIndex("MST", axis) & (1 << 5)) != 0;
                while (value == true && count > 0)
                {
                    count--;
                    Thread.Sleep(10);
                    value = (ReadIntIndex("MST", axis) & (1 << 5)) != 0;
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("IsMotorMoving", "错误信息：检查运动状态失败！" + ex.Message);
            }
        }
        public bool IsMotorEnable(int axis)
        {
            try
            {
                return (_acs_api.GetMotorState((ACS.SPiiPlusNET.Axis)axis) & MotorStates.ACSC_MST_ENABLE) != 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("IsMotorEnable", "错误信息：检查运动使能状态失败！" + ex.Message);
            }
        }
        public bool IsMotorHome(int axis)
        {
            // bool isHome = (ReadIntIndex("MFLAGS", axis) & (1 << 3)) != 0;
            // return isHome;

            //int ret = ReadInt(LaserCutterParameter.Instance.axisParameter.GetAxisItem(axis).AcsCommand_Home);
            //int ret = ReadInt($"MFLAGS({axis}).#HOME");

            //if (ret == 1)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return false;
        }


        public void SetAcceleration(int axis, double acc)
        {
            try
            {
                if (!IsMotorEnable(axis))
                    return;

                _acs_api.SetAcceleration((ACS.SPiiPlusNET.Axis)axis, acc);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("SetAcceleration", "错误信息：设置加速度失败！" + ex.Message);
            }
        }
        public double GetAcceleration(int axis)
        {
            try
            {
                return _acs_api.GetAcceleration((ACS.SPiiPlusNET.Axis)axis);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("GetAcceleration", "错误信息：获取加速度失败！" + ex.Message);
            }
        }
        public void SetDeceleration(int axis, double dec)
        {
            try
            {
                if (!IsMotorEnable(axis))
                    return;

                _acs_api.SetDeceleration((ACS.SPiiPlusNET.Axis)axis, dec);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("SetDeceleration", "错误信息：设置减速度失败！" + ex.Message);
            }
        }
        public double GetDeceleration(int axis)
        {
            try
            {
                return _acs_api.GetDeceleration((ACS.SPiiPlusNET.Axis)axis);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("GetDeceleration", "错误信息：设置减速度失败！" + ex.Message);
            }
        }

        public void SetJerk(int axis, double jerk)
        {
            try
            {
                if (!IsMotorEnable(axis))
                    return;

                _acs_api.SetJerk((ACS.SPiiPlusNET.Axis)axis, jerk);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("SetJerk", "错误信息：设置jerk速度失败！" + ex.Message);
            }
        }
        public double GetJerk(int axis)
        {
            try
            {
                return _acs_api.GetJerk((ACS.SPiiPlusNET.Axis)axis);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("GetJerk", "错误信息：获取jerk速度失败！" + ex.Message);
            }
        }
        public void SetVelocity(int axis, double vel)
        {
            try
            {
                if (!IsMotorEnable(axis))
                    return;

                _acs_api.SetVelocity((ACS.SPiiPlusNET.Axis)axis, vel);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("SetVelocity", "错误信息：设置速度失败！" + ex.Message);
            }
        }
        public double GetVelocity(int axis)
        {
            try
            {
                return _acs_api.GetVelocity((ACS.SPiiPlusNET.Axis)axis);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("GetVelocity", "错误信息：获取速度失败！" + ex.Message);
            }
        }

        public void SetRelativePosition(int axis, double pos)
        {
            try
            {
                if (!_isConnect)
                {
                    throw new AcsMotionException("<SetRelativePosition>", "ACS未连接");
                }
                if (!IsMotorEnable(axis))
                {
                    throw new AcsMotionException("<SetRelativePosition>", string.Format("{0}轴未上使能，请先上使能", ((AxisIndex)axis).ToString()));
                }
                if (!IsMotorHome(axis))
                {
                    throw new AcsMotionException("<SetRelativePosition>", string.Format("{0}轴还没回过原点，请回原点后再试", ((AxisIndex)axis).ToString()));
                }
                if (IsMotorMoving(axis))
                {
                    throw new AcsMotionException("<SetRelativePosition>", string.Format("{0}轴还在运动中，请稍后再试", ((AxisIndex)axis).ToString()));
                }

                _acs_api.ToPoint(MotionFlags.ACSC_AMF_RELATIVE, (ACS.SPiiPlusNET.Axis)axis, pos);
                //_acs_api.WaitMotionEnd((ACS.SPiiPlusNET.Axis)axis, int.MaxValue);
                //_acs_api.ToPointAsync(MotionFlags.ACSC_AMF_RELATIVE, (ACS.SPiiPlusNET.Axis)axis, pos);
            }
            catch (Exception ex)
            {
                throw new AcsMotionException("SetAbsolutePosition", "错误信息：相对运动失败！" + ex.Message);
                //throw ex;
            }
        }
        public void SetAbsolutePosition(int axis, double pos)
        {
            try
            {
                if (!_isConnect)
                {
                    throw new AcsMotionException("<SetAbsolutePosition>", "ACS未连接");
                }
                if (!IsMotorEnable(axis))
                {
                    throw new AcsMotionException("<SetAbsolutePosition>", string.Format("{0}轴未上使能，请先上使能", ((AxisIndex)axis).ToString()));
                }
                if (!IsMotorHome(axis))
                {
                    throw new AcsMotionException("<SetAbsolutePosition>", string.Format("{0}轴还没回过原点，请回原点后再试", ((AxisIndex)axis).ToString()));
                }
                if (IsMotorMoving(axis))
                {
                    throw new AcsMotionException("<SetAbsolutePosition>", string.Format("{0}轴还在运动中，请稍后再试", ((AxisIndex)axis).ToString()));
                }
                _acs_api.ToPoint(MotionFlags.ACSC_NONE, (ACS.SPiiPlusNET.Axis)axis, pos);

                //_acs_api.WaitMotionEnd((ACS.SPiiPlusNET.Axis)axis, int.MaxValue);
                //_acs_api.ToPointAsync(MotionFlags.ACSC_NONE, (ACS.SPiiPlusNET.Axis)axis, pos); 
            }
            catch (Exception ex)
            {
                throw new AcsMotionException("SetAbsolutePosition", "错误信息：绝对运动失败！" + ex.Message);
                // throw ex;
            }
        }
        public void SetAbsolutePosition222(int axis, double pos)
        {
            _acs_api.ToPoint(MotionFlags.ACSC_NONE, (ACS.SPiiPlusNET.Axis)axis, pos);
        }
        public double GetAbsolutePosition(int axis)
        {
            try
            {
                //return _acs_api.GetFPosition((ACS.SPiiPlusNET.Axis)axis);
                return this.ReadDoubleIndex("APOS", axis);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("GetAbsolutePosition", "错误信息：获取绝对位置失败！" + ex.Message);
            }
        }


        public void WaitPosition(int axis, int timeout = int.MaxValue)
        {
            try
            {
                if (!IsMotorEnable(axis))
                    return;
                _acs_api.WaitMotionEnd((ACS.SPiiPlusNET.Axis)axis, timeout);
            }
            catch (Exception ex)
            {
                throw new AcsMotionException("WaitPosition", "错误信息：等待运动完成失败！" + ex.Message);
            }
        }
        public void JogMotor(int axis, double vel)
        {
            try
            {
                //if (!_isConnect)
                //{
                //    throw new AcsMotionException("<JogMotor>", "ACS未连接");
                //}
                //if (!IsMotorEnable(axis))
                //{
                //    throw new AcsMotionException("<JogMotor>", string.Format("{0}轴未上使能，请先上使能", ((AxisIndex)axis).ToString()));
                //}
                //if (!IsMotorHome(axis))
                //{
                //    throw new AcsMotionException("<JogMotor>", string.Format("{0}轴还没回过原点，请回原点后再试", ((AxisIndex)axis).ToString()));
                //}
                //if (IsMotorMoving(axis))
                //{
                //    throw new AcsMotionException("<JogMotor>", string.Format("{0}轴还在运动中，请稍后再试", ((AxisIndex)axis).ToString()));
                //}
                _acs_api.Jog(MotionFlags.ACSC_AMF_VELOCITY, (ACS.SPiiPlusNET.Axis)axis, vel);
            }
            catch (Exception ex)
            {
                throw new AcsMotionException("<JogMotor>", "错误信息：Jog运动失败！" + ex.Message);
            }
        }
        public void HaltAllMotor(int axis)
        {
            //throw new NotImplementedException();
        }
        public void HaltMotor(int axis)
        {
            try
            {
                if (!IsMotorEnable(axis))
                    return;
                _acs_api.Halt((ACS.SPiiPlusNET.Axis)axis);
            }
            catch (Exception ex)
            {
                throw new AcsMotionException("HaltMotor", "错误信息：停止运动失败！" + ex.Message);
            }
        }
        public void EnableMotor(int axis, bool status)
        {
            try
            {
                if (status)
                {
                    _acs_api.Enable((ACS.SPiiPlusNET.Axis)axis);
                }
                else
                {
                    _acs_api.Disable((ACS.SPiiPlusNET.Axis)axis);
                }
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("EnableMotor", "错误信息：启用轴失败！" + ex.Message);
            }
        }
        public void DisableAll()
        {
            try
            {
                this._acs_api.DisableAll();
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("DisableAll", "错误信息：禁用所有轴失败！" + ex.Message);
            }
        }

        // io part
        public double ReadDouble(string variableName)
        {
            try
            {
                var result = _acs_api.ReadVariable(variableName);
                return Convert.ToDouble(result);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("ReadDouble", "错误信息：读取变量值失败！" + ex.Message);
            }
        }
        public double ReadDoubleIndex(string variableName, int index)
        {
            try
            {
                var result = _acs_api.ReadVariable(variableName, ProgramBuffer.ACSC_NONE, index, index);
                return Convert.ToDouble(result);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("ReadDoubleIndex", "错误信息：读取变量值失败！" + ex.Message);
            }
        }
        public double ReadDoubleBuffer(string variableName, int bufferId)
        {
            try
            {
                var result = _acs_api.ReadVariable(variableName, (ProgramBuffer)bufferId);
                return Convert.ToDouble(result);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("ReadDoubleBuffer", "错误信息：读取buffer失败！" + ex.Message);
            }
        }

        public int ReadInt(string variableName)
        {
            try
            {
                var result = _acs_api.ReadVariable(variableName);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("ReadInt", "错误信息：读取变量值失败！" + ex.Message);
            }
        }
        public int ReadIntIndex(string variableName, int index)
        {
            try
            {
                var result = _acs_api.ReadVariable(variableName, ProgramBuffer.ACSC_NONE, index, index);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("ReadIntIndex", "错误信息：读取变量值失败！" + ex.Message);
            }
        }
        public int ReadIntBuffer(string variableName, int bufferId)
        {
            try
            {
                var result = _acs_api.ReadVariable(variableName, (ProgramBuffer)bufferId);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("ReadIntBuffer", "错误信息：读取buffer里面变量值失败！" + ex.Message);
            }
        }

        public void WriteVariable(string variableName, object value)
        {
            try
            {
                _acs_api.WriteVariable(value, variableName);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("WriteVariable", "错误信息：写入变量值失败！" + ex.Message);
            }
        }
        public void WriteArryVariable(string variableName, object value, int startIndex, int stopIndex)
        {
            try
            {
                _acs_api.WriteVariable(value, variableName, ProgramBuffer.ACSC_NONE, startIndex, stopIndex);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("WriteVariable", "错误信息：写入变量值失败！" + ex.Message);
            }
        }

        public void WriteVariableIndex(string variableName, int index, object value)
        {
            try
            {
                _acs_api.WriteVariable(value, variableName, ProgramBuffer.ACSC_NONE, index, index);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("WriteVariableIndex", "错误信息：写入变量值失败！" + ex.Message);
            }
        }
        public void WriteVariableBuffer(string variableName, int bufferId, object value)
        {
            try
            {
                _acs_api.WriteVariable(value, variableName, (ProgramBuffer)bufferId);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("WriteVariableBuffer", "错误信息：写入buffer变量值失败！" + ex.Message);
            }
        }

        public void RunBuffer(int bufidx, string bufname)
        {
            try
            {
                this._acs_api.RunBuffer((ProgramBuffer)bufidx, bufname);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("RunBuffer", "错误信息：运行buffer失败！" + ex.Message);
            }
        }
        public void RunBufferAsync(int bufidx, string bufname)
        {
            try
            {
                this._acs_api.RunBufferAsync((ProgramBuffer)bufidx, bufname);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("RunBufferAsync", "错误信息：异步运行buffer失败！" + ex.Message);
            }
        }
        public void WaitBufferEnd(int bufidx, int timeout = int.MaxValue)
        {
            try
            {
                this._acs_api.WaitProgramEnd((ProgramBuffer)bufidx, timeout);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("WaitBufferEnd", "错误信息：等待buffer执行完成失败！" + ex.Message);
            }
        }

        public void FaultClear(int axis)
        {
            try
            {
                this._acs_api.FaultClear((ACS.SPiiPlusNET.Axis)axis);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("FaultClear", "错误信息：清除错误失败！" + ex.Message);
            }
        }
        public void EnableFaultEvent(int axis)
        {
            this._acs_api.SYSTEMERROR -= SystemErrorAction;
            this._acs_api.SYSTEMERROR += SystemErrorAction;
            this._acs_api.EnableEvent(Interrupts.ACSC_INTR_SYSTEM_ERROR);
            this._acs_api.SetInterruptMask(Interrupts.ACSC_INTR_SYSTEM_ERROR, (uint)Math.Pow(2, axis));
        }
        public void DisableFaultEvent(int axis)
        {
            this._acs_api.SYSTEMERROR -= SystemErrorAction;
        }

        private void SystemErrorAction(ulong param)
        {
            MessageBox.Show("SystemError," + (SafetyControlMasks)param);
        }

        public void SetSLLimit(int axis, double pos)
        {
            try
            {
                this._acs_api.WriteVariable(pos, "SLLIMIT", ProgramBuffer.ACSC_NONE, axis, axis);
            }
            catch (AcsReadWriteException ex)
            {
                throw ex;
            }

        }
        public void SetSRLimit(int axis, double pos)
        {
            try
            {
                this._acs_api.WriteVariable(pos, "SRLIMIT", ProgramBuffer.ACSC_NONE, axis, axis);
            }
            catch (AcsReadWriteException ex)
            {
                throw ex;
            }

        }
        public void EnableSoftWareLimit(int axis)
        {
            try
            {
                var faultMask = this._acs_api.GetFaultMask((ACS.SPiiPlusNET.Axis)axis);
                faultMask = faultMask | SafetyControlMasks.ACSC_SAFETY_SLL | SafetyControlMasks.ACSC_SAFETY_SRL;
                this._acs_api.SetFaultMask((ACS.SPiiPlusNET.Axis)axis, faultMask | SafetyControlMasks.ACSC_SAFETY_SLL | SafetyControlMasks.ACSC_SAFETY_SRL);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("EnableSoftWareLimit", "错误信息：启用软极限失败" + ex.Message);
            }
        }
        public void DisableSoftWareLimit(int axis)
        {
            try
            {
                var faultMask = this._acs_api.GetFaultMask((ACS.SPiiPlusNET.Axis)axis);
                this._acs_api.SetFaultMask((ACS.SPiiPlusNET.Axis)axis, faultMask & ~SafetyControlMasks.ACSC_SAFETY_SLL & ~SafetyControlMasks.ACSC_SAFETY_SRL);
            }
            catch (Exception ex)
            {
                throw new AcsReadWriteException("DisableSoftWareLimit", "错误信息：禁用软极限失败" + ex.Message);
            }
        }

        public void WriteLocalVariable(string variableName, int bufferId, object value)
        {
            try
            {
                WriteVariableBuffer(variableName, bufferId, value);
            }
            catch (AcsReadWriteException ex)
            {
                throw ex;
            }
        }
        public void WriteGlobalVariable(string variableName, int index, object value)
        {
            try
            {
                WriteVariableIndex(variableName, index, value);
            }
            catch (AcsReadWriteException ex)
            {
                throw ex;
            }
        }

        public void MoveGroupAbsolution(int[] axis, double[] points)
        {
            try
            {
                if (!_isConnect)
                {
                    throw new AcsMotionException("<MoveGroupAbsolution>", "ACS未连接");
                }

                ACS.SPiiPlusNET.Axis[] axisGroup = new ACS.SPiiPlusNET.Axis[axis.Length + 1];
                for (int i = 0; i < axis.Length; i++)
                {
                    axisGroup[i] = (ACS.SPiiPlusNET.Axis)axis[i];

                    if (!IsMotorEnable(axis[i]))
                    {
                        throw new AcsMotionException("<MoveGroupAbsolution>", string.Format("{0}轴未上使能，请先上使能", ((AxisIndex)axis[i]).ToString()));
                    }
                    if (IsMotorMoving(axis[i]))
                    {
                        throw new AcsMotionException("<MoveGroupAbsolution>", string.Format("{0}轴还在运动中，请稍后再试", ((AxisIndex)axis[i]).ToString()));
                    }
                }
                axisGroup[axis.Length] = ACS.SPiiPlusNET.Axis.ACSC_NONE;
                this._acs_api.ToPointM(MotionFlags.ACSC_NONE, axisGroup, points);
                this._acs_api.EndSequenceM(axisGroup);
            }
            catch (Exception ex)
            {
                throw new AcsMotionException("<MoveGroupAbsolution>", "错误信息：群组运动失败， " + ex.ToString());
            }

        }
        public void MoveGroupRelativePosition(int[] axis, double[] points)
        {
            try
            {
                if (!_isConnect)
                {
                    throw new AcsMotionException("<MoveGroupRelativePosition>", "ACS未连接");
                }
                ACS.SPiiPlusNET.Axis[] axisGroup = new ACS.SPiiPlusNET.Axis[axis.Length + 1];
                for (int i = 0; i < axis.Length; i++)
                {
                    axisGroup[i] = (ACS.SPiiPlusNET.Axis)axis[i];
                    if (!IsMotorEnable(axis[i]))
                    {
                        throw new AcsMotionException("<MoveGroupRelativePosition>", string.Format("{0}轴未上使能，请先上使能", ((AxisIndex)axis[i]).ToString()));
                    }
                    if (IsMotorMoving(axis[i]))
                    {
                        throw new AcsMotionException("<MoveGroupRelativePosition>", string.Format("{0}轴还在运动中，请稍后再试", ((AxisIndex)axis[i]).ToString()));
                    }
                    if (!IsMotorHome(axis[i]))
                    {
                        throw new AcsMotionException("<MoveGroupRelativePosition>", string.Format("{0}轴还没回过原点，请回原点后再试", ((AxisIndex)axis[i]).ToString()));
                    }
                }
                axisGroup[axis.Length] = ACS.SPiiPlusNET.Axis.ACSC_NONE;
                this._acs_api.ToPointM(MotionFlags.ACSC_AMF_RELATIVE, axisGroup, points);
                this._acs_api.EndSequenceM(axisGroup);
            }
            catch (Exception ex)
            {
                throw new AcsMotionException("<MoveGroupRelativePosition>", "错误信息：群组运动失败， " + ex.ToString());
            }
        }
        public void WaitGroupMotionEnd(int[] axis)
        {
            try
            {
                ACS.SPiiPlusNET.Axis[] axisGroup = new ACS.SPiiPlusNET.Axis[axis.Length];
                for (int i = 0; i < axis.Length; i++)
                {
                    axisGroup[i] = (ACS.SPiiPlusNET.Axis)axis[i];
                    this._acs_api.WaitMotionEnd(axisGroup[i], int.MaxValue);
                }
            }
            catch (Exception ex)
            {
                throw new AcsMotionException("WaitGroupMotionEnd", "错误信息：群组运动到位失败， " + ex.ToString());
            }
        }

        public void WriteArrayVariable(string variableName, object value, int startIndex, int stopIndex)
        {
            _acs_api.WriteVariable(value, variableName, ProgramBuffer.ACSC_NONE, startIndex, stopIndex);
        }

        public void MoveArc1(int[] axis, double[] firstpoint, double[] centerpoint, double[] finalpoint, bool bclockwise = true)
        {
            try
            {
                if (!_isConnect)
                {
                    throw new AcsMotionException("<MoveArc1>", "ACS未连接");
                }
                ACS.SPiiPlusNET.Axis[] axisGroup = new ACS.SPiiPlusNET.Axis[axis.Length + 1];
                for (int i = 0; i < axis.Length; i++)
                {
                    axisGroup[i] = (ACS.SPiiPlusNET.Axis)axis[i];
                    if (!IsMotorEnable(axis[i]))
                    {
                        throw new AcsMotionException("<MoveArc1>", string.Format("{0}轴未上使能，请先上使能", ((AxisIndex)axis[i]).ToString()));
                    }
                    if (IsMotorMoving(axis[i]))
                    {
                        throw new AcsMotionException("<MoveArc1>", string.Format("{0}轴还在运动中，请稍后再试", ((AxisIndex)axis[i]).ToString()));
                    }
                    if (!IsMotorHome(axis[i]))
                    {
                        throw new AcsMotionException("<MoveArc1>", string.Format("{0}轴还没回过原点，请回原点后再试", ((AxisIndex)axis[i]).ToString()));
                    }
                }
                axisGroup[axis.Length] = ACS.SPiiPlusNET.Axis.ACSC_NONE;
                _acs_api.Segment(MotionFlags.ACSC_NONE, axisGroup, firstpoint);
                if (bclockwise)
                {
                    _acs_api.SegmentArc1(MotionFlags.ACSC_NONE, axisGroup, centerpoint, finalpoint, RotationDirection.ACSC_CLOCKWISE, Api.ACSC_NONE, Api.ACSC_NONE, null, null, Api.ACSC_NONE, null);
                }
                else
                {
                    _acs_api.SegmentArc1(MotionFlags.ACSC_NONE, axisGroup, centerpoint, finalpoint, RotationDirection.ACSC_COUNTERCLOCKWISE, Api.ACSC_NONE, Api.ACSC_NONE, null, null, Api.ACSC_NONE, null);
                }

                this._acs_api.EndSequenceM(axisGroup);
            }
            catch (Exception ex)
            {
                throw new AcsMotionException("<MoveArc1>", "错误信息：MoveArc1 fail， " + ex.ToString());
            }


        }
        public void MoveArc2(int[] axis, double[] firstpoint, double[] centerpoint, double radian)
        {
            try
            {
                if (!_isConnect)
                {
                    throw new AcsMotionException("<MoveArc2>", "ACS未连接");
                }
                ACS.SPiiPlusNET.Axis[] axisGroup = new ACS.SPiiPlusNET.Axis[axis.Length + 1];
                for (int i = 0; i < axis.Length; i++)
                {
                    axisGroup[i] = (ACS.SPiiPlusNET.Axis)axis[i];
                    if (!IsMotorEnable(axis[i]))
                    {
                        throw new AcsMotionException("<MoveArc2>", string.Format("{0}轴未上使能，请先上使能", ((AxisIndex)axis[i]).ToString()));
                    }
                    if (IsMotorMoving(axis[i]))
                    {
                        throw new AcsMotionException("<MoveArc2>", string.Format("{0}轴还在运动中，请稍后再试", ((AxisIndex)axis[i]).ToString()));
                    }
                    if (!IsMotorHome(axis[i]))
                    {
                        throw new AcsMotionException("<MoveArc2>", string.Format("{0}轴还没回过原点，请回原点后再试", ((AxisIndex)axis[i]).ToString()));
                    }
                }
                axisGroup[axis.Length] = ACS.SPiiPlusNET.Axis.ACSC_NONE;
                _acs_api.Segment(MotionFlags.ACSC_NONE, axisGroup, firstpoint);
                _acs_api.SegmentArc2(MotionFlags.ACSC_NONE, axisGroup, centerpoint, radian, Api.ACSC_NONE, Api.ACSC_NONE, null, null, Api.ACSC_NONE, null);
                this._acs_api.EndSequenceM(axisGroup);
            }
            catch (Exception ex)
            {
                throw new AcsMotionException("<MoveArc2>", "错误信息：MoveArc1 fail， " + ex.ToString());
            }


        }

        public bool CheckIsBufferEnd(int bufidx)
        {
            bool isEnd = false;
            isEnd = (ReadIntIndex("PST", bufidx) & (1 << 1)) == 0;
            return isEnd;
        }

        public void MultPoint(int[] axis, List<double> axis1, List<double> axis2)
        {
            ACS.SPiiPlusNET.Axis[] axisGroup = new ACS.SPiiPlusNET.Axis[axis.Length + 1];
            for (int i = 0; i < axis.Length; i++)
            {
                axisGroup[i] = (ACS.SPiiPlusNET.Axis)axis[i];
                if (!IsMotorEnable(axis[i]))
                {
                    throw new AcsMotionException("<MoveArc2>", string.Format("{0}轴未上使能，请先上使能", ((AxisIndex)axis[i]).ToString()));
                }
                if (IsMotorMoving(axis[i]))
                {
                    throw new AcsMotionException("<MoveArc2>", string.Format("{0}轴还在运动中，请稍后再试", ((AxisIndex)axis[i]).ToString()));
                }
                if (!IsMotorHome(axis[i]))
                {
                    throw new AcsMotionException("<MoveArc2>", string.Format("{0}轴还没回过原点，请回原点后再试", ((AxisIndex)axis[i]).ToString()));
                }
            }
            axisGroup[axis.Length] = ACS.SPiiPlusNET.Axis.ACSC_NONE;
            _acs_api.EnableM(axisGroup);
            _acs_api.MultiPointM(MotionFlags.ACSC_AMF_WAIT, axisGroup, 0);
            double[] poit = { 0, 0 };
            for (int i = 0; i < axis1.Count; i++)
            {
                poit[0] = axis1[i];
                poit[1] = axis2[i];
                try
                {
                    // _acs_api.ExtAddPointM(axisGroup, poit, 0.1);
                    _acs_api.AddPointM(axisGroup, poit);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"第{i}点：" + e.Message);
                    _acs_api.DisableM(axisGroup);
                    goto Flag;
                }
            }
            _acs_api.EndSequenceM(axisGroup);
            _acs_api.GoM(axisGroup);
            Flag:;
        }
        public void MultPoint(int[] axis, List<Point> pos)
        {
            ACS.SPiiPlusNET.Axis[] axisGroup = new ACS.SPiiPlusNET.Axis[axis.Length + 1];
            for (int i = 0; i < axis.Length; i++)
            {
                axisGroup[i] = (ACS.SPiiPlusNET.Axis)axis[i];
                if (!IsMotorEnable(axis[i]))
                {
                    throw new AcsMotionException("<MoveArc2>", string.Format("{0}轴未上使能，请先上使能", ((AxisIndex)axis[i]).ToString()));
                }
                if (IsMotorMoving(axis[i]))
                {
                    throw new AcsMotionException("<MoveArc2>", string.Format("{0}轴还在运动中，请稍后再试", ((AxisIndex)axis[i]).ToString()));
                }
                if (!IsMotorHome(axis[i]))
                {
                    throw new AcsMotionException("<MoveArc2>", string.Format("{0}轴还没回过原点，请回原点后再试", ((AxisIndex)axis[i]).ToString()));
                }
            }
            axisGroup[axis.Length] = ACS.SPiiPlusNET.Axis.ACSC_NONE;
            _acs_api.EnableM(axisGroup);
            _acs_api.MultiPointM(MotionFlags.ACSC_AMF_WAIT, axisGroup, 0);
            double[] poit = { 0, 0 };
            for (int i = 0; i < pos.Count; i++)
            {
                poit[0] = pos[i].X;
                poit[1] = pos[i].Y;
                try
                {
                    _acs_api.AddPointM(axisGroup, poit);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"第{i}点：" + e.Message);
                    _acs_api.DisableM(axisGroup);
                    goto Flag;
                }
            }
            _acs_api.EndSequenceM(axisGroup);
            _acs_api.GoM(axisGroup);
            Flag:;
        }

        #region 平台相关处理函数

        public void MovePathTest()
        {
            try
            {
                //int[] axis = new int[2];
                //axis[0] = 0;
                //axis[1] = 2;
                //double[] firstpoint = new double[2];
                //firstpoint[0] = 100;
                //firstpoint[1] = 200;
                //double[] centerpoint = new double[2];
                //centerpoint[0] = 100;
                //centerpoint[1] = 210;
                //double radian = -3.1415926;
                //double[] point1 = new double[2];
                //point1[0] = 120;
                //point1[1] = 220;
                //double[] point2 = new double[2];
                //point2[0] = 120;
                //point2[1] = 210;
                //double radian2 = -3.1415926;
                //ACS.SPiiPlusNET.Axis[] axisGroup = new ACS.SPiiPlusNET.Axis[axis.Length + 1];
                //for (int i = 0; i < axis.Length; i++)
                //{
                //    axisGroup[i] = (ACS.SPiiPlusNET.Axis)axis[i];
                //}
                //axisGroup[axis.Length] = ACS.SPiiPlusNET.Axis.ACSC_NONE;
                //_acs_api.Segment(MotionFlags.ACSC_NONE, axisGroup, firstpoint);
                //_acs_api.SegmentArc2(MotionFlags.ACSC_NONE, axisGroup, centerpoint, radian, Api.ACSC_NONE, Api.ACSC_NONE, null, null, Api.ACSC_NONE, null);
                //_acs_api.SegmentLine(MotionFlags.ACSC_NONE, axisGroup, point1, Api.ACSC_NONE, Api.ACSC_NONE, null, null, Api.ACSC_NONE, null);
                //_acs_api.SegmentArc2(MotionFlags.ACSC_NONE, axisGroup, point2, radian2, Api.ACSC_NONE, Api.ACSC_NONE, null, null, Api.ACSC_NONE, null);
                //_acs_api.SegmentLine(MotionFlags.ACSC_NONE, axisGroup, firstpoint, Api.ACSC_NONE, Api.ACSC_NONE, null, null, Api.ACSC_NONE, null);
                //this._acs_api.EndSequenceM(axisGroup);

                int[] axis = new int[2];
                axis[0] = 0;
                axis[1] = 2;
                ACS.SPiiPlusNET.Axis[] axisGroup = new ACS.SPiiPlusNET.Axis[axis.Length + 1];
                for (int i = 0; i < axis.Length; i++)
                {
                    axisGroup[i] = (ACS.SPiiPlusNET.Axis)axis[i];
                }
                axisGroup[axis.Length] = ACS.SPiiPlusNET.Axis.ACSC_NONE;


                double[] firstpoint = new double[2];
                firstpoint[0] = 100;
                firstpoint[1] = 200;
                double[] centerpoint1 = new double[2];
                centerpoint1[0] = 100;
                centerpoint1[1] = 220;
                double radian1 = -3.1415926;
                double[] linepoint1 = new double[2];
                linepoint1[0] = 120;
                linepoint1[1] = 240;
                double[] centerpoint2 = new double[2];
                centerpoint2[0] = 120;
                centerpoint2[1] = 220;
                double radian2 = -3.1415926;

                _acs_api.Segment(MotionFlags.ACSC_NONE, axisGroup, firstpoint);
                _acs_api.SegmentArc2(MotionFlags.ACSC_NONE, axisGroup, centerpoint1, radian1, Api.ACSC_NONE, Api.ACSC_NONE, null, null, Api.ACSC_NONE, null);
                _acs_api.SegmentLine(MotionFlags.ACSC_NONE, axisGroup, linepoint1, Api.ACSC_NONE, Api.ACSC_NONE, null, null, Api.ACSC_NONE, null);
                _acs_api.SegmentArc2(MotionFlags.ACSC_NONE, axisGroup, centerpoint2, radian2, Api.ACSC_NONE, Api.ACSC_NONE, null, null, Api.ACSC_NONE, null);
                _acs_api.SegmentLine(MotionFlags.ACSC_NONE, axisGroup, firstpoint, Api.ACSC_NONE, Api.ACSC_NONE, null, null, Api.ACSC_NONE, null);
                this._acs_api.EndSequenceM(axisGroup);
            }
            catch (Exception ex)
            {
                throw new AcsMotionException("MovePathTest", "错误信息：MoveArc1 fail， " + ex.ToString());
            }
        }

        //public void MovePath(int[] axis, List<CuttingPathItem> cuttingPathItems )
        //{
        //    try
        //    {
        //        ACS.SPiiPlusNET.Axis[] axisGroup = new ACS.SPiiPlusNET.Axis[axis.Length + 1];
        //        for (int i = 0; i < axis.Length; i++)
        //        {
        //            axisGroup[i] = (ACS.SPiiPlusNET.Axis)axis[i];
        //        }
        //        axisGroup[axis.Length] = ACS.SPiiPlusNET.Axis.ACSC_NONE;

        //        double[] firstpoint = new double[2];
        //        firstpoint[0] = cuttingPathItems[0].startPos[0];
        //        firstpoint[1] = cuttingPathItems[0].startPos[1];
        //        _acs_api.Segment(MotionFlags.ACSC_NONE, axisGroup, firstpoint);
        //        for (int i = 0; i < cuttingPathItems.Count; i++)
        //        {
        //            double[] point = new double[2];
        //            if (cuttingPathItems[i].type == CuttingPathItem.PathType.Arc)
        //            {
        //                point[0] = cuttingPathItems[i].centPos[0];
        //                point[1] = cuttingPathItems[i].centPos[1];
        //                _acs_api.SegmentArc2(MotionFlags.ACSC_NONE, axisGroup, point, cuttingPathItems[i].angle, Api.ACSC_NONE, Api.ACSC_NONE, null, null, Api.ACSC_NONE, null);
        //            }
        //            else if(cuttingPathItems[i].type == CuttingPathItem.PathType.Line)
        //            {
        //                point[0] = cuttingPathItems[i].endPos[0];
        //                point[1] = cuttingPathItems[i].endPos[1];
        //                _acs_api.SegmentLine(MotionFlags.ACSC_NONE, axisGroup, point, Api.ACSC_NONE, Api.ACSC_NONE, null, null, Api.ACSC_NONE, null);
        //            }
        //            Thread.Sleep(20);
        //        }
        //        this._acs_api.EndSequenceM(axisGroup);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new AcsMotionException("MovePathTest", "错误信息：MoveArc1 fail， " + ex.ToString());
        //    }
        //}

        internal void ConnectIP(object laserCutterParameter, string acsIpAddress)
        {
            throw new NotImplementedException();
        }

        #endregion

        //public void Home(int axis)
        //{
        //    try
        //    {
        //        if (!_isConnect)
        //        {
        //            throw new AcsMotionException("<Home>", "ACS未连接");
        //        }
        //        if (!IsMotorEnable(axis))
        //        {
        //            throw new AcsMotionException("<Home>", string.Format("{0}轴未上使能，请先上使能", ((AxisIndex)axis).ToString()));
        //        }
        //        if (IsMotorMoving(axis))
        //        {
        //            throw new AcsMotionException("<Home>", string.Format("{0}轴还在运动中，请稍后再试", ((AxisIndex)axis).ToString()));
        //        }

        //        switch (axis)
        //        {
        //            case (int)AxisIndex.AXIS_INDEX_Y:
        //            if (!CheckIsBufferEnd(4))
        //            {
        //                throw new AcsMotionException("<Home>", string.Format("{0}轴回零的buffer4尚未执行完毕，请稍后再试", ((AxisIndex)axis).ToString()));
        //            }
        //            this._acs_api.RunBuffer(ProgramBuffer.ACSC_BUFFER_4, "YHOME");
        //            break;
        //            case (int)AxisIndex.AXIS_INDEX_X1:
        //            if (!CheckIsBufferEnd(3))
        //            {
        //                throw new AcsMotionException("<Home>", string.Format("{0}轴回零的buffer3尚未执行完毕，请稍后再试", ((AxisIndex)axis).ToString()));
        //            }
        //            this._acs_api.RunBuffer(ProgramBuffer.ACSC_BUFFER_3, "XHOME");
        //            break;
        //            case (int)AxisIndex.AXIS_INDEX_Z1:
        //            if (!CheckIsBufferEnd(5))
        //            {
        //                throw new AcsMotionException("<Home>", string.Format("{0}轴回零的buffer5尚未执行完毕，请稍后再试", ((AxisIndex)axis).ToString()));
        //            }
        //            this._acs_api.RunBuffer(ProgramBuffer.ACSC_BUFFER_5, "HomeZ");
        //            break;
        //        }
        //        IsHome[axis] = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new AcsMotionException("<Home>", "错误信息：回零失败！" + ex.Message);
        //        //throw ex;
        //    }
        //}

        //public uint GetMotorState(int axis)
        //{
        //    uint iState = 0x0000;
        //    try
        //    {
        //        IndexStates State = _acs_api.GetIndexState((ACS.SPiiPlusNET.Axis)axis);
        //        if (0 != (State & IndexStates.ACSC_IST_IND))
        //            iState |= 0x002;

        //        SafetyControlMasks fault = _acs_api.GetFault((ACS.SPiiPlusNET.Axis)axis);
        //        if (0 != (fault & SafetyControlMasks.ACSC_SAFETY_RL))
        //            iState |= 0x008;
        //        if (0 != (fault & SafetyControlMasks.ACSC_SAFETY_LL))
        //            iState |= 0x004;

        //        MotorStates MotorState = _acs_api.GetMotorState((ACS.SPiiPlusNET.Axis)axis);
        //        if (0 != (MotorState & MotorStates.ACSC_MST_ENABLE))
        //            iState |= 0x010;
        //        if (0 != (MotorState & MotorStates.ACSC_MST_MOVE))
        //            iState |= 0x020;
        //    }
        //    catch
        //    {

        //    }
        //    return iState;
        //}
        //public void ControlVacuum(bool On)
        //{
        //    try
        //    {
        //        if (On)
        //        {
        //            this._acs_api.RunBuffer(ProgramBuffer.ACSC_BUFFER_8, "Vacuum_ON");
        //        }
        //        else
        //        {
        //            this._acs_api.RunBuffer(ProgramBuffer.ACSC_BUFFER_8, "Vacuum_OFF");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new AcsMotionException("ControlLaser", "错误信息：ControlLaser fail， " + ex.ToString());
        //    }


        //}
        //public bool AllMotorHome()
        //{
        //    this._acs_api.RunBuffer(ProgramBuffer.ACSC_BUFFER_2, "ALLHOME");
        //    //检测所有轴是否都回到了原点
        //    return true;
        //}
        //public void AllMotorEnable()
        //{
        //    this._acs_api.RunBuffer(ProgramBuffer.ACSC_BUFFER_3, "EnableStart");
        //}

        //public void ControlLaser(bool On)
        //{
        //    try
        //    {
        //        if (On)
        //        {
        //            this._acs_api.RunBuffer(ProgramBuffer.ACSC_BUFFER_5, "PegFunctionActive");
        //            int count = 2000;
        //            while (count > 0)
        //            {
        //                Thread.Sleep(50);
        //                if (ReadIntBuffer("complete", 5) == 1)
        //                    break;
        //                count--;
        //            }
        //        }
        //        else
        //        {
        //            this._acs_api.RunBuffer(ProgramBuffer.ACSC_BUFFER_5, "PegFunctionDeactive");
        //            int count = 2000;
        //            while (count > 0)
        //            {
        //                Thread.Sleep(50);
        //                if (ReadIntBuffer("complete", 5) == 1)
        //                    break;
        //                count--;
        //            }
        //        }
        //        //if(On)
        //        // {
        //        //     this._acs_api.RunBuffer(ProgramBuffer.ACSC_BUFFER_6, "Laser_On");
        //        //     int count = 2000;
        //        //     while (count > 0)
        //        //     {
        //        //         Thread.Sleep(50);
        //        //         if (ReadIntBuffer("complete", 6) == 1)
        //        //             break;
        //        //         count--;
        //        //     }
        //        // }
        //        // else
        //        // {
        //        //     this._acs_api.RunBuffer(ProgramBuffer.ACSC_BUFFER_6, "Laser_Off");
        //        //     int count = 2000;
        //        //     while (count > 0)
        //        //     {
        //        //         Thread.Sleep(50);
        //        //         if (ReadIntBuffer("complete", 6) == 1)
        //        //             break;
        //        //         count--;
        //        //     }
        //        // }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new AcsMotionException("ControlLaser", "错误信息：ControlLaser fail， " + ex.ToString());
        //    }
        //}
    }
}
