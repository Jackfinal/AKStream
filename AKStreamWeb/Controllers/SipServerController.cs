using System.Collections.Generic;
using AKStreamWeb.Attributes;
using AKStreamWeb.Services;
using LibCommon;
using LibCommon.Structs;
using LibCommon.Structs.GB28181;
using LibCommon.Structs.WebRequest;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AKStreamWeb.Controllers
{
    [Log]
    [AuthVerify]
    [ApiController]
    [Route("/SipGate")]
    [SwaggerTag("Sip网关相关接口")]
    public class SipServerController : ControllerBase
    {
        /// <summary>
        /// 获取Sip设备的历史录像文件列表
        /// </summary>
        /// <param name="AccessKey"></param>
        /// <param name="deviceId"></param>
        /// <param name="channelId"></param>
        /// <param name="queryRecordFile"></param>
        /// <returns>返回true/false,如果是true,则从SipChannel的LastRecordInfos字段中获取历史文件列表，历史文件可能很多，加载时间会比较长</returns>
        /// <exception cref="AkStreamException"></exception>
        [Route("GetHistroyRecordFileList")]
        [HttpPost]
        public bool GetHistroyRecordFileList(
            [FromHeader(Name = "AccessKey")] string AccessKey, string deviceId, string channelId,
            SipQueryRecordFile queryRecordFile)
        {
            ResponseStruct rs;
            var ret = SipServerService.GetHistroyRecordFileList(deviceId, channelId, queryRecordFile, out rs);
            if (!rs.Code.Equals(ErrorNumber.None))
            {
                throw new AkStreamException(rs);
            }

            return ret;
        }


        /// <summary>
        /// ptz控制接口
        /// </summary>
        /// <param name="AccessKey"></param>
        /// <param name="ptzCmd"></param>
        /// <returns></returns>
        /// <exception cref="AkStreamException"></exception>
        [Route("PtzCtrl")]
        [HttpPost]
        public bool PtzCtrl(
            [FromHeader(Name = "AccessKey")] string AccessKey, ReqPtzCtrl ptzCmd)
        {
            ResponseStruct rs;
            var ret = SipServerService.PtzCtrl(ptzCmd, out rs);
            if (!rs.Code.Equals(ErrorNumber.None))
            {
                throw new AkStreamException(rs);
            }

            return ret;
        }

        /// <summary>
        /// 请求gb28181直播流
        /// </summary>
        /// <param name="AccessKey"></param>
        /// <param name="deviceId"></param>
        /// <param name="channelId"></param>
        /// <param name="rtpPort">设置成null(不传),或者0，将自动申请rtp端口，否则需要由应用自行申请rtp端口后填入</param>
        /// <returns>流媒体的相关访问信息</returns>
        /// <exception cref="AkStreamException"></exception>
        [Route("LiveVideo")]
        [HttpGet]
        public MediaServerStreamInfo LiveVideo(
            [FromHeader(Name = "AccessKey")] string AccessKey, string deviceId, string channelId, ushort? rtpPort = 0)
        {
            ResponseStruct rs;
            var ret = SipServerService.LiveVideo(deviceId, channelId, out rs, rtpPort);
            if (!rs.Code.Equals(ErrorNumber.None))
            {
                throw new AkStreamException(rs);
            }

            return ret;
        }

        /// <summary>
        /// 停止GB28181直播流
        /// </summary>
        /// <param name="AccessKey"></param>
        /// <param name="deviceId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        /// <exception cref="AkStreamException"></exception>
        [Route("StopLiveVideo")]
        [HttpGet]
        public bool StopLiveVideo(
            [FromHeader(Name = "AccessKey")] string AccessKey, string deviceId, string channelId)
        {
            ResponseStruct rs;
            var ret = SipServerService.StopLiveVideo(deviceId, channelId, out rs);
            if (!rs.Code.Equals(ErrorNumber.None))
            {
                throw new AkStreamException(rs);
            }

            return ret;
        }

        /// <summary>
        /// 获取指定通道是否正在推流
        /// </summary>
        /// <param name="AccessKey"></param>
        /// <param name="deviceId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        /// <exception cref="AkStreamException"></exception>
        [Route("IsLiveVideo")]
        [HttpGet]
        public bool IsLiveVideo(
            [FromHeader(Name = "AccessKey")] string AccessKey, string deviceId, string channelId)
        {
            ResponseStruct rs;
            var ret = SipServerService.IsLiveVideo(deviceId, channelId, out rs);
            if (!rs.Code.Equals(ErrorNumber.None))
            {
                throw new AkStreamException(rs);
            }

            return ret;
        }

        /// <summary>
        /// 根据ID获取SipChannel
        /// </summary>
        /// <param name="AccessKey"></param>
        /// <param name="deviceId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        /// <exception cref="AkStreamException"></exception>
        [Route("GetSipChannelById")]
        [HttpGet]
        public SipChannel GetSipChannelById(
            [FromHeader(Name = "AccessKey")] string AccessKey, string deviceId, string channelId)
        {
            ResponseStruct rs;
            var ret = SipServerService.GetSipChannelById(deviceId, channelId, out rs);
            if (!rs.Code.Equals(ErrorNumber.None))
            {
                throw new AkStreamException(rs);
            }

            return ret;
        }

        /// <summary>
        /// 通过deviceId获取sip设备实例
        /// </summary>
        /// <param name="AccessKey"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        /// <exception cref="AkStreamException"></exception>
        [Route("GetSipDeviceListByDeviceId")]
        [HttpGet]
        public SipDevice GetSipDeviceListByDeviceId(
            [FromHeader(Name = "AccessKey")] string AccessKey, string deviceId)
        {
            ResponseStruct rs;
            var ret = SipServerService.GetSipDeviceListByDeviceId(deviceId, out rs);
            if (!rs.Code.Equals(ErrorNumber.None))
            {
                throw new AkStreamException(rs);
            }

            return ret;
        }

        /// <summary>
        /// 获取Sip设备列表
        /// </summary>
        /// <param name="AccessKey"></param>
        /// <returns></returns>
        /// <exception cref="AkStreamException"></exception>
        [Route("GetSipDeviceList")]
        [HttpGet]
        public List<SipDevice> GetSipDeviceList(
            [FromHeader(Name = "AccessKey")] string AccessKey)
        {
            ResponseStruct rs;
            var ret = SipServerService.GetSipDeviceList(out rs);
            if (!rs.Code.Equals(ErrorNumber.None))
            {
                throw new AkStreamException(rs);
            }

            return ret;
        }
    }
}